using KnowledgeBase.Common.Messages;
using RawRabbit;
using System.Reflection;
using System.Threading.Tasks;

namespace KnowledgeBase.Common.RabbitMQ
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBusClient _busClient;
        private readonly string _defaultNamespace;

        public BusPublisher(RabbitMqOptions options, IBusClient busClient)
        {
            _busClient = busClient;
            _defaultNamespace = options.Namespace;
        }

        public async Task SendAsync<TCommand>(TCommand command)
            where TCommand : ICommand
            => await _busClient.PublishAsync(command, ctx => ctx.UsePublishConfiguration(p => p.WithRoutingKey(GetRoutingKey(@command))));

        public async Task PublishAsync<TEvent>(TEvent @event)
            where TEvent : IEvent
            => await _busClient.PublishAsync(@event, ctx => ctx.UsePublishConfiguration(p => p.WithRoutingKey(GetRoutingKey(@event))));

        private string GetRoutingKey<T>(T message)
        {
            var @namespace = message.GetType().GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace ??
                             _defaultNamespace;
            @namespace = string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";

            return $"{@namespace}{typeof(T).Name.Underscore()}".ToLowerInvariant();
        }
    }
}
