using KnowledgeBase.Common.Messages;
using System;

namespace KnowledgeBase.Common.RabbitMQ
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand> onError = null)
            where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent> onError = null)
            where TEvent : IEvent;
    }
}
