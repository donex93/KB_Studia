using KnowledgeBase.API.Models.Articles;
using KnowledgeBase.API.Queries;
using RestEase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeBase.API.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IArticlesService
    {
        [AllowAnyStatusCode]
        [Get("articles/{id}")]
        Task<Article> GetAsync([Path] Guid id);

        [AllowAnyStatusCode]
        [Get("articles")]
        Task<IEnumerable<Article>> BrowseAsync([Query] BrowseArticles query);
    }
}
