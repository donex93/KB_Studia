using KnowledgeBase.Common.Messages;
using System.Threading.Tasks;

namespace KnowledgeBase.Common.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
