using KnowledgeBase.Common.Messages;
using Newtonsoft.Json;
using System;

namespace KnowledgeBase.Services.Articles.Messages.Events
{
    public class ArticleUpdated : IEvent
    {
        public Guid Id { get; }
        public string Name { get; }

        [JsonConstructor]
        public ArticleUpdated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
