using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Santiago.Infrastructure.Repositories.DataProviding.DbEntities
{
    [Table("dbo.Page")]
    public class DbPage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Alias { get; set; }

        public string Text { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual DbPage Parent { get; set; }

        public string SitemapXmlChangeFrequency { get; set; }

        public float? SitemapXmlPriority { get; set; }

        public int TemplateId { get; set; }

        [ForeignKey("TemplateId")]
        public virtual DbPageTemplate Template { get; set; }

        public bool IsPublished { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public DateTime? PublicationDate { get; set; }
    }
}