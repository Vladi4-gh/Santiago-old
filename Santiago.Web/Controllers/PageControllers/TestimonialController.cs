using System.Web.Mvc;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Services;
using Santiago.Web.Controllers.BaseControllers;
using Santiago.Web.Helpers;

namespace Santiago.Web.Controllers.PageControllers
{
    public class TestimonialController : PageBaseController
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet]
        public ActionResult Index(Page page)
        {
            ViewBag.MetaDescription = page.MetaDescription;
            ViewBag.MetaKeywords = page.MetaKeywords;
            ViewBag.PageTitle = page.Title;
            ViewBag.Testimonials = _testimonialService.GetAllTestimonialsOrderedByCreationDateDesc().ToViewModelList(x => x.ToTestimonialViewModel());

            return View();
        }
    }
}