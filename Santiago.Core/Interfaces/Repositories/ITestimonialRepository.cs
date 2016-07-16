using System.Collections.Generic;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Repositories
{
    public interface ITestimonialRepository
    {
        Testimonial GetTestimonialById(int id);

        IEnumerable<Testimonial> GetAllTestimonialsOrderedByCreationDateDesc();

        IEnumerable<Testimonial> GetTestimonialsOrderedByCreationDateDesc(int skipNumber, int takeNumber);

        int GetTestimonialsTotalCount();

        int CreateTestimonial(Testimonial testimonial);

        void UpdateTestimonial(Testimonial testimonial);

        void DeleteTestimonial(int id);
    }
}