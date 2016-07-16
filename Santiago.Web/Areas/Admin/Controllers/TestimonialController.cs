using System.Web.Mvc;
using Santiago.Core.Interfaces.Services;
using Santiago.Web.Areas.Admin.ViewModels.Common;
using Santiago.Web.Areas.Admin.ViewModels.Testimonial;
using Santiago.Web.Attributes.ActionFilterAttributes;
using Santiago.Web.Controllers.BaseControllers;
using Santiago.Web.Helpers;

namespace Santiago.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class TestimonialController : BaseController
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Отзывы";

            return View();
        }

        [HttpGet]
        public JsonResult GetTestimonialsOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            var testimonialsTotalCount = _testimonialService.GetTestimonialsTotalCount();
            var model = new DataTableViewModel<int, TestimonialDataTableItemViewModel>
            {
                ItemsTotalCount = testimonialsTotalCount,
                Items = _testimonialService.GetTestimonialsOrderedByCreationDateDesc(DataTableHelper.GetRightSkipNumber(skipNumber, takeNumber, testimonialsTotalCount), takeNumber).ToViewModelList(x => x.ToTestimonialDataTableItemViewModel())
            };

            return Json(SerializeObjectToJson(model), JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void CreateTestimonial(AddTestimonialViewModel model)
        {
            _testimonialService.CreateTestimonial(model.ToTestimonial());
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void UpdateTestimonial(EditTestimonialViewModel model)
        {
            _testimonialService.UpdateTestimonial(model.ToTestimonial());
        }

        [HttpPost]
        public void DeleteTestimonial(int id)
        {
            _testimonialService.DeleteTestimonial(id);
        }
    }
}