using KnowledgeBase.Common.Handlers;
using KnowledgeBase.Services.Articles.Data;
using KnowledgeBase.Services.Articles.Domain;
using KnowledgeBase.Services.Articles.Queries;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KnowledgeBase.Services.Articles.Handlers
{
    public class GetArticleHandler : IQueryHandler<GetArticle, Article>
    {
        private readonly ArticlesDbContext _context;

        public GetArticleHandler(ArticlesDbContext context)
        {
            _context = context;
        }
        public async Task<Article> HandleAsync(GetArticle query)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(x => x.Id == query.Id);

            return article;
        }
    }
}
