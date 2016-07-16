using System;
using Santiago.Web.Areas.Admin.ViewModels.ImageFile;

namespace Santiago.Web.Areas.Admin.ViewModels.Testimonial
{
    public class TestimonialDataTableItemViewModel
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public int AuthorImageFileId { get; set; }

        public ImageFileDataTableItemViewModel AuthorImageFile { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}