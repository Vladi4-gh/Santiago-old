using System.Web.Mvc;
using Santiago.Core.Interfaces.Services;
using Santiago.Web.Areas.Admin.ViewModels.Common;
using Santiago.Web.Areas.Admin.ViewModels.PhotographCategory;
using Santiago.Web.Attributes.ActionFilterAttributes;
using Santiago.Web.Controllers.BaseControllers;
using Santiago.Web.Helpers;

namespace Santiago.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class PhotographCategoryController : BaseController
    {
        private readonly IPhotographCategoryService _photographCategoryService;

        public PhotographCategoryController(IPhotographCategoryService photographCategoryService)
        {
            _photographCategoryService = photographCategoryService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Категории фотографий";

            return View();
        }

        [HttpGet]
        public JsonResult GetPhotographCategoriesOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            var photographCategoriesTotalCount = _photographCategoryService.GetPhotographCategoriesTotalCount();
            var model = new DataTableViewModel<int, PhotographCategoryDataTableItemViewModel>
            {
                ItemsTotalCount = photographCategoriesTotalCount,
                Items = _photographCategoryService.GetPhotographCategoriesOrderedByCreationDateDesc(DataTableHelper.GetRightSkipNumber(skipNumber, takeNumber, photographCategoriesTotalCount), takeNumber).ToViewModelList(x => x.ToPhotographCategoryDataTableItemViewModel())
            };

            return Json(SerializeObjectToJson(model), JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void CreatePhotographCategory(AddPhotographCategoryViewModel model)
        {
            _photographCategoryService.CreatePhotographCategory(model.ToPhotographCategory());
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void UpdatePhotographCategory(EditPhotographCategoryViewModel model)
        {
            _photographCategoryService.UpdatePhotographCategory(model.ToPhotographCategory());
        }

        [HttpPost]
        public void DeletePhotographCategory(int id)
        {
            _photographCategoryService.DeletePhotographCategory(id);
        }
    }
}