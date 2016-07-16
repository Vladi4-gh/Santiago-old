using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Santiago.Infrastructure.Repositories.DataProviding.DbEntities
{
    [Table("dbo.Testimonial")]
    public class DbTestimonial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public int AuthorImageFileId { get; set; }

        [ForeignKey("AuthorImageFileId")]
        public virtual DbImageFile AuthorImageFile { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}