using System.Web.Mvc;
using Santiago.Core.Interfaces.Services;
using Santiago.Web.Areas.Admin.ViewModels.Common;
using Santiago.Web.Areas.Admin.ViewModels.Photograph;
using Santiago.Web.Attributes.ActionFilterAttributes;
using Santiago.Web.Controllers.BaseControllers;
using Santiago.Web.Helpers;

namespace Santiago.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class PhotographController : BaseController
    {
        private readonly IPhotographService _photographService;

        public PhotographController(IPhotographService photographService)
        {
            _photographService = photographService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Фотографии";

            return View();
        }

        [HttpGet]
        public JsonResult GetPhotographsOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            var photographsTotalCount = _photographService.GetPhotographsTotalCount();
            var model = new DataTableViewModel<int, PhotographDataTableItemViewModel>
            {
                ItemsTotalCount = photographsTotalCount,
                Items = _photographService.GetPhotographsOrderedByCreationDateDesc(DataTableHelper.GetRightSkipNumber(skipNumber, takeNumber, photographsTotalCount), takeNumber).ToViewModelList(x => x.ToPhotographDataTableItemViewModel())
            };

            return Json(SerializeObjectToJson(model), JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void CreatePhotograph(AddPhotographViewModel model)
        {
            _photographService.CreatePhotograph(model.ToPhotograph());
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void UpdatePhotograph(EditPhotographViewModel model)
        {
            _photographService.UpdatePhotograph(model.ToPhotograph());
        }

        [HttpPost]
        public void DeletePhotograph(int id)
        {
            _photographService.DeletePhotograph(id);
        }
    }
}