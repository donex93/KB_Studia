using System;

namespace KnowledgeBase.Services.Articles.Domain
{
    public class Article
    {
        public Guid Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public string Name { get; private set; }
        public string Content { get; private set; }

        public Article(Guid id, string name, string content)
        {
            Id = id;
            CreatedDate = DateTime.UtcNow;
            SetUpdatedDate();

            SetName(name);
            SetContent(content);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Article name cannot be empty.");
            }

            Name = name.Trim();
            SetUpdatedDate();
        }

        public void SetContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new Exception("Article content cannot be empty.");
            }

            Content = content.Trim();
            SetUpdatedDate();
        }

        private void SetUpdatedDate()
            => UpdatedDate = DateTime.UtcNow;
    }
}
