using KnowledgeBase.Common.Types;
using KnowledgeBase.Services.Articles.Domain;
using System;

namespace KnowledgeBase.Services.Articles.Queries
{
    public class GetArticle : IQuery<Article>
    {
        public Guid Id { get; set; }
    }
}
