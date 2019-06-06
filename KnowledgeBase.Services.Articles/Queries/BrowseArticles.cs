using KnowledgeBase.Common.Types;
using KnowledgeBase.Services.Articles.Domain;

namespace KnowledgeBase.Services.Articles.Queries
{
    public class BrowseArticles : PagedQueryBase, IQuery<PagedResult<Article>>
    {
    }
}
