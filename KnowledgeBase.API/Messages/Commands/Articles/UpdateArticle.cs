using KnowledgeBase.Common.Messages;
using Newtonsoft.Json;
using System;

namespace KnowledgeBase.API.Messages.Commands.Articles
{
    [MessageNamespace("articles")]
    public class UpdateArticle : ICommand
    {
        public Guid Id { get; private set; }
        public string Name { get; }
        public string Content { get; }

        [JsonConstructor]
        public UpdateArticle(Guid id, string name,
            string content)
        {
            Id = id;
            Name = name;
            Content = content;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
