using KnowledgeBase.Common.Messages;
using Newtonsoft.Json;
using System;

namespace KnowledgeBase.Services.Articles.Messages.Events
{
    public class ArticleCreated : IEvent
    {
        public Guid Id { get; }
        public string Name { get; }

        [JsonConstructor]
        public ArticleCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
