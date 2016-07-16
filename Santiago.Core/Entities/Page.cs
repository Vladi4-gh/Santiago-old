using System;

namespace Santiago.Core.Entities
{
    public class Page
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Alias { get; set; }

        public string Text { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public int? ParentId { get; set; }

        public Page Parent { get; set; }

        public string SitemapXmlChangeFrequency { get; set; }

        public float? SitemapXmlPriority { get; set; }

        public int TemplateId { get; set; }

        public PageTemplate Template { get; set; }

        public bool IsPublished { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public DateTime? PublicationDate { get; set; }
    }
}