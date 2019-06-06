using KnowledgeBase.Services.Articles.Domain;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Services.Articles.Data
{
    public class ArticlesDbContext : DbContext
    {
        public ArticlesDbContext(DbContextOptions<ArticlesDbContext> options)
            : base(options)
        { }

        public DbSet<Article> Articles { get; set; }
    }
}
