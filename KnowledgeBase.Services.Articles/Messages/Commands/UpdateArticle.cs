using KnowledgeBase.Common.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Services.Articles.Messages.Commands
{
    public class UpdateArticle : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Content { get; }

        [JsonConstructor]
        public UpdateArticle(Guid id, string name, string content)
        {
            Id = id;
            Name = name;
            Content = content;
        }
    }
}
