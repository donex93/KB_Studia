using KnowledgeBase.Common.Handlers;
using KnowledgeBase.Services.Articles.Data;
using KnowledgeBase.Services.Articles.Messages.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace KnowledgeBase.Services.Articles.Handlers
{
    public class CreateArticleHandler : ICommandHandler<CreateArticle>
    {
        private readonly IConfiguration _configuration;

        public CreateArticleHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task HandleAsync(CreateArticle command)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<ArticlesDbContext>();
                optionsBuilder.UseMySql(_configuration.GetConnectionString("Default"));

                using (var context = new ArticlesDbContext(optionsBuilder.Options))
                {
                    context.Articles.Add(new Domain.Article(command.Id, command.Name, command.Content));
                    await context.SaveChangesAsync();
                }
            }
            catch (System.Exception ex)
            {

                throw;
            }
            
        }
    }
}
