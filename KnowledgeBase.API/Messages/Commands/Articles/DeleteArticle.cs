using KnowledgeBase.Common.Messages;
using Newtonsoft.Json;
using System;

namespace KnowledgeBase.API.Messages.Commands.Articles
{
    [MessageNamespace("articles")]
    public class DeleteArticle : ICommand
    {
        public Guid Id { get; }

        [JsonConstructor]
        public DeleteArticle(Guid id)
        {
            Id = id;
        }
    }
}
