using Autofac;
using KnowledgeBase.Common.Handlers;
using KnowledgeBase.Common.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace KnowledgeBase.Common.RabbitMQ
{
    public static class Extensions
    {
        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app)
            => new BusSubscriber(app);

        public static void AddRabbitMq(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<RabbitMqOptions>("rabbitMq");

                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<RawRabbitConfiguration>("rabbitMq");

                return options;
            }).SingleInstance();

            var assembly = Assembly.GetCallingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>))
                .InstancePerDependency();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerDependency();
            builder.RegisterType<Handler>().As<IHandler>()
                .InstancePerDependency();
            builder.RegisterType<BusPublisher>().As<IBusPublisher>()
                .InstancePerDependency();

            ConfigureBus(builder);
        }

        private static void ConfigureBus(ContainerBuilder builder)
        {
            builder.Register<IInstanceFactory>(context =>
            {
                var options = context.Resolve<RabbitMqOptions>();
                var configuration = context.Resolve<RawRabbitConfiguration>();
                var namingConventions = new CustomNamingConventions(options.Namespace);

                return RawRabbitFactory.CreateInstanceFactory(new RawRabbitOptions
                {
                    DependencyInjection = ioc =>
                    {
                        ioc.AddSingleton(options);
                        ioc.AddSingleton(configuration);
                        ioc.AddSingleton<INamingConventions>(namingConventions);
                    },
                    Plugins = p => p
                        .UseAttributeRouting()
                        .UseRetryLater()
                        .UseContextForwarding()
                });
            }).SingleInstance();
            builder.Register(context => context.Resolve<IInstanceFactory>().Create());
        }

        private class CustomNamingConventions : NamingConventions
        {
            public CustomNamingConventions(string defaultNamespace)
            {
                ExchangeNamingConvention = type => GetNamespace(type, defaultNamespace).ToLowerInvariant();
                RoutingKeyConvention = type =>
                    $"#.{GetRoutingKeyNamespace(type, defaultNamespace)}{type.Name.Underscore()}".ToLowerInvariant();
                ErrorExchangeNamingConvention = () => $"{defaultNamespace}.error";
                RetryLaterExchangeConvention = span => $"{defaultNamespace}.retry";
                RetryLaterQueueNameConvetion = (exchange, span) =>
                    $"{defaultNamespace}.retry_for_{exchange.Replace(".", "_")}_in_{span.TotalMilliseconds}_ms".ToLowerInvariant();
            }

            private static string GetRoutingKeyNamespace(Type type, string defaultNamespace)
            {
                var @namespace = type.GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace ?? defaultNamespace;

                return string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";
            }

            private static string GetNamespace(Type type, string defaultNamespace)
            {
                var @namespace = type.GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace ?? defaultNamespace;

                return string.IsNullOrWhiteSpace(@namespace) ? "#" : $"{@namespace}";
            }
        }
    }

    
}
