using System;

namespace Santiago.Web.ViewModels.Testimonial
{
    public class TestimonialViewModel
    {
        public string AuthorName { get; set; }

        public TestimonialAuthorImageFileViewModel AuthorImageFile { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }
    }
}