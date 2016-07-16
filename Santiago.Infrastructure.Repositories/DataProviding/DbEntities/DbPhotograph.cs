using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Santiago.Infrastructure.Repositories.DataProviding.DbEntities
{
    [Table("dbo.Photograph")]
    public class DbPhotograph
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int GalleryItemImageFileId { get; set; }

        [ForeignKey("GalleryItemImageFileId")]
        public virtual DbImageFile GalleryItemImageFile { get; set; }

        public int GallerySliderImageFileId { get; set; }

        [ForeignKey("GallerySliderImageFileId")]
        public virtual DbImageFile GallerySliderImageFile { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual DbPhotographCategory Category { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}