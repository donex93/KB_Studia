using KnowledgeBase.Common.Messages;
using System.Threading.Tasks;

namespace KnowledgeBase.Common.RabbitMQ
{
    public interface IBusPublisher
    {
        Task SendAsync<TCommand>(TCommand command)
            where TCommand : ICommand;

        Task PublishAsync<TEvent>(TEvent @event)
            where TEvent : IEvent;
    }
}
