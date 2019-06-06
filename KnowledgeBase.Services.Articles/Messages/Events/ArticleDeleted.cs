using KnowledgeBase.Common.Messages;
using Newtonsoft.Json;
using System;

namespace KnowledgeBase.Services.Articles.Messages.Events
{
    public class ArticleDeleted : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ArticleDeleted(Guid id)
        {
            Id = id;
        }
    }
}
