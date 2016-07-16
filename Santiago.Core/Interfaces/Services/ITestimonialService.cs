using System.Collections.Generic;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Services
{
    public interface ITestimonialService
    {
        Testimonial GetTestimonialById(int id);

        List<Testimonial> GetAllTestimonialsOrderedByCreationDateDesc();

        List<Testimonial> GetTestimonialsOrderedByCreationDateDesc(int skipNumber, int takeNumber);

        int GetTestimonialsTotalCount();

        int CreateTestimonial(Testimonial testimonial);

        void UpdateTestimonial(Testimonial testimonial);

        void DeleteTestimonial(int id);
    }
}