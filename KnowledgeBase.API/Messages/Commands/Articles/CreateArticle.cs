using KnowledgeBase.Common.Messages;
using Newtonsoft.Json;
using System;

namespace KnowledgeBase.API.Messages.Commands.Articles
{
    [MessageNamespace("articles")]
    public class CreateArticle : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Content { get; }

        [JsonConstructor]
        public CreateArticle(Guid id, string name, string content)
        {
            Id = id;
            Name = name;
            Content = content;
        }
    }
}
