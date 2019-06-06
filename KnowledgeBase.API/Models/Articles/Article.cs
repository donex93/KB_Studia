using System;

namespace KnowledgeBase.API.Models.Articles
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
