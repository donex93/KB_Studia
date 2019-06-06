using KnowledgeBase.Common.Handlers;
using KnowledgeBase.Services.Articles.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Services.Articles.Handlers
{
    public class DeleteArticleHandler : ICommandHandler<DeleteArticle>
    {
        public Task HandleAsync(DeleteArticle command)
        {
            throw new NotImplementedException();
        }
    }
}
