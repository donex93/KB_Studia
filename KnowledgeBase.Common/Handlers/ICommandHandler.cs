using KnowledgeBase.Common.Messages;
using System.Threading.Tasks;

namespace KnowledgeBase.Common.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
