using KnowledgeBase.Common.Messages;
using Newtonsoft.Json;
using System;

namespace KnowledgeBase.Services.Articles.Messages.Commands
{
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
