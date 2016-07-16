using System;

namespace Santiago.Core.Entities
{
    public class Photograph
    {
        public int Id { get; set; }

        public int GalleryItemImageFileId { get; set; }

        public ImageFile GalleryItemImageFile { get; set; }

        public int GallerySliderImageFileId { get; set; }

        public ImageFile GallerySliderImageFile { get; set; }

        public int? CategoryId { get; set; }

        public PhotographCategory Category { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}