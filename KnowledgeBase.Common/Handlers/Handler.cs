using System;
using System.Threading.Tasks;

namespace KnowledgeBase.Common.Handlers
{
    public class Handler : IHandler
    {
        private Func<Task> _handle;

        public IHandler Handle(Func<Task> handle)
        {
            _handle = handle;

            return this;
        }

        public async Task ExecuteAsync()
        {
            await _handle();
        }
    }
}

