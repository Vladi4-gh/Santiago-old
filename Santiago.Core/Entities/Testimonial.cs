using System;

namespace Santiago.Core.Entities
{
    public class Testimonial
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public int AuthorImageFileId { get; set; }

        public ImageFile AuthorImageFile { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}