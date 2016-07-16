using System.Collections.Generic;
using System.Linq;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Repositories;
using Santiago.Core.Interfaces.Services;
using Santiago.Infrastructure.Services.Helpers;

namespace Santiago.Infrastructure.Services
{
    public class TestimonialService : ITestimonialService
    {
        private readonly ITestimonialRepository _testimonialRepository;

        private readonly IFileService _fileService;

        public TestimonialService(ITestimonialRepository testimonialRepository, IFileService fileService)
        {
            _testimonialRepository = testimonialRepository;
            _fileService = fileService;
        }

        public Testimonial GetTestimonialById(int id)
        {
            return _testimonialRepository.GetTestimonialById(id);
        }

        public List<Testimonial> GetAllTestimonialsOrderedByCreationDateDesc()
        {
            var testimonials = _testimonialRepository.GetAllTestimonialsOrderedByCreationDateDesc().ToList();

            foreach (var testimonial in testimonials)
            {
                testimonial.AuthorImageFile.Url = UploadFolderPathsHelper.UploadImagesFolderPath + testimonial.AuthorImageFile.Name;
            }

            return testimonials;
        }

        public List<Testimonial> GetTestimonialsOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            var testimonials = _testimonialRepository.GetTestimonialsOrderedByCreationDateDesc(skipNumber, takeNumber).ToList();

            foreach (var testimonial in testimonials)
            {
                testimonial.AuthorImageFile.Url = UploadFolderPathsHelper.UploadImagesFolderPath + testimonial.AuthorImageFile.Name;
            }

            return testimonials;
        }

        public int GetTestimonialsTotalCount()
        {
            return _testimonialRepository.GetTestimonialsTotalCount();
        }

        public int CreateTestimonial(Testimonial testimonial)
        {
            return _testimonialRepository.CreateTestimonial(testimonial);
        }

        public void UpdateTestimonial(Testimonial testimonial)
        {
            _testimonialRepository.UpdateTestimonial(testimonial);
        }

        public void DeleteTestimonial(int id)
        {
            var selectedTestimonial = _testimonialRepository.GetTestimonialById(id);

            if (selectedTestimonial != null)
            {
                _testimonialRepository.DeleteTestimonial(selectedTestimonial.Id);

                _fileService.DeleteImageFile(selectedTestimonial.AuthorImageFileId);
            }
        }
    }
}