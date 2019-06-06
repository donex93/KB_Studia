using KnowledgeBase.Common.Handlers;
using KnowledgeBase.Services.Articles.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Services.Articles.Handlers
{
    public class UpdateArticleHandler : ICommandHandler<UpdateArticle>
    {
        public Task HandleAsync(UpdateArticle command)
        {
            throw new NotImplementedException();
        }
    }
}
