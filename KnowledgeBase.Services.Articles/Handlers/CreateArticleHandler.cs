using KnowledgeBase.Common.Handlers;
using KnowledgeBase.Services.Articles.Data;
using KnowledgeBase.Services.Articles.Messages.Commands;
using System.Threading.Tasks;

namespace KnowledgeBase.Services.Articles.Handlers
{
    public class CreateArticleHandler : ICommandHandler<CreateArticle>
    {
        private readonly ArticlesDbContext _context;

        public CreateArticleHandler(ArticlesDbContext context)
        {
            _context = context;
        }
        public async Task HandleAsync(CreateArticle command)
        {
            _context.Articles.Add(new Domain.Article(command.Id, command.Name, command.Content));

            await _context.SaveChangesAsync();
        }
    }
}
