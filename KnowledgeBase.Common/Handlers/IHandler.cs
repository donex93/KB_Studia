using System;
using System.Threading.Tasks;

namespace KnowledgeBase.Common.Handlers
{
    public interface IHandler
    {
        IHandler Handle(Func<Task> handle);
        Task ExecuteAsync();
    }
}
