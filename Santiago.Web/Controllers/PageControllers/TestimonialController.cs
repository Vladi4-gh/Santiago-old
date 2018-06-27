using System.Web.Mvc;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Services;
using Santiago.Web.Controllers.BaseControllers;
using Santiago.Web.Helpers;

namespace Santiago.Web.Controllers.PageControllers
{
    public class SantiagoWebTestimonialController : PageBaseController
    {
        private readonly ITestimonialService _santiagoWebTestimonialControllerTestimonialService;

        public SantiagoWebTestimonialController(ITestimonialService santiagoWebTestimonialControllerTestimonialService)
        {
            _santiagoWebTestimonialControllerTestimonialService = santiagoWebTestimonialControllerTestimonialService;
        }

        [HttpGet]
        public ActionResult Index(Page santiagoWebTestimonialControllerPage)
        {
            ViewBag.MetaDescription = santiagoWebTestimonialControllerPage.MetaDescription;
            ViewBag.MetaKeywords = santiagoWebTestimonialControllerPage.MetaKeywords;
            ViewBag.PageTitle = santiagoWebTestimonialControllerPage.Title;
            ViewBag.Testimonials = _santiagoWebTestimonialControllerTestimonialService.GetAllTestimonialsOrderedByCreationDateDesc().ToViewModelList(santiagoWebTestimonialControllerX => santiagoWebTestimonialControllerX.ToTestimonialViewModel());

            return View();
        }
    }
}