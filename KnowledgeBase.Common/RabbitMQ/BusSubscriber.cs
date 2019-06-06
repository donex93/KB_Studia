using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using RawRabbit.Common;
using System.Threading.Tasks;
using Polly;
using KnowledgeBase.Common.Messages;
using RawRabbit;
using KnowledgeBase.Common.Handlers;

namespace KnowledgeBase.Common.RabbitMQ
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _defaultNamespace;
        private readonly int _retries;
        private readonly int _retryInterval;
        private readonly IBusClient _busClient;

        public BusSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            var options = _serviceProvider.GetService<RabbitMqOptions>();
            _defaultNamespace = options.Namespace;
            _retries = options.Retries >= 0 ? options.Retries : 3;
            _retryInterval = options.RetryInterval > 0 ? options.RetryInterval : 2;
            _busClient = _serviceProvider.GetService<IBusClient>();
        }

        public IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand> onError = null)
            where TCommand : ICommand
        {
            _busClient.SubscribeAsync<TCommand>(async (command) =>
            {
                var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

                return await TryHandleAsync(command, () => commandHandler.HandleAsync(command), onError);
            },
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                    cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>(@namespace, queueName)))));

            return this;
        }

        public IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent> onError = null)
            where TEvent : IEvent
        {
            _busClient.SubscribeAsync<TEvent>(async (@event) =>
            {
                var eventHandler = _serviceProvider.GetService<IEventHandler<TEvent>>();

                return await TryHandleAsync(@event, () => eventHandler.HandleAsync(@event), onError);
            },
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                    cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>(@namespace, queueName)))));

            return this;
        }

        private async Task<Acknowledgement> TryHandleAsync<TMessage>(TMessage message,
            Func<Task> handle, Func<TMessage> onError = null)
        {
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(_retries, i => TimeSpan.FromSeconds(_retryInterval));

            return await retryPolicy.ExecuteAsync<Acknowledgement>(async () =>
            {
                await handle();

                return new Ack();
            });
        }

        private string GetQueueName<T>(string @namespace = null, string name = null)
        {
            @namespace = string.IsNullOrWhiteSpace(@namespace)
                ? (string.IsNullOrWhiteSpace(_defaultNamespace) ? string.Empty : _defaultNamespace)
                : @namespace;

            var separatedNamespace = string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";

            return (string.IsNullOrWhiteSpace(name)
                ? $"{Assembly.GetEntryAssembly().GetName().Name}/{separatedNamespace}{typeof(T).Name.Underscore()}"
                : $"{name}/{separatedNamespace}{typeof(T).Name.Underscore()}").ToLowerInvariant();
        }
    }
}
