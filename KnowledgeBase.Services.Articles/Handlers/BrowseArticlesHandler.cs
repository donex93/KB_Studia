using KnowledgeBase.Common.Handlers;
using KnowledgeBase.Common.Types;
using KnowledgeBase.Services.Articles.Domain;
using KnowledgeBase.Services.Articles.Queries;
using System;
using System.Threading.Tasks;

namespace KnowledgeBase.Services.Articles.Handlers
{
    public class BrowseArticlesHandler : IQueryHandler<BrowseArticles, PagedResult<Article>>
    {
        public Task<PagedResult<Article>> HandleAsync(BrowseArticles query)
        {
            throw new NotImplementedException();
        }
    }
}
