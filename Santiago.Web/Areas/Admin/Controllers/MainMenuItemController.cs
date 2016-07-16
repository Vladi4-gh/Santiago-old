using System.Web.Mvc;
using Santiago.Core.Interfaces.Services;
using Santiago.Web.Areas.Admin.ViewModels.Common;
using Santiago.Web.Areas.Admin.ViewModels.MainMenuItem;
using Santiago.Web.Attributes.ActionFilterAttributes;
using Santiago.Web.Controllers.BaseControllers;
using Santiago.Web.Helpers;

namespace Santiago.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class MainMenuItemController : BaseController
    {
        private readonly IMainMenuItemService _mainMenuItemService;

        public MainMenuItemController(IMainMenuItemService mainMenuItemService)
        {
            _mainMenuItemService = mainMenuItemService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Главное меню";

            return View();
        }

        [HttpGet]
        public JsonResult GetMainMenuItemsOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            var mainMenuItemsTotalCount = _mainMenuItemService.GetMainMenuItemsTotalCount();
            var model = new DataTableViewModel<int, MainMenuItemDataTableItemViewModel>
            {
                ItemsTotalCount = mainMenuItemsTotalCount,
                Items = _mainMenuItemService.GetMainMenuItemsOrderedByCreationDateDesc(DataTableHelper.GetRightSkipNumber(skipNumber, takeNumber, mainMenuItemsTotalCount), takeNumber).ToViewModelList(x => x.ToMainMenuItemDataTableItemViewModel())
            };

            return Json(SerializeObjectToJson(model), JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void CreateMainMenuItem(AddMainMenuItemViewModel model)
        {
            _mainMenuItemService.CreateMainMenuItem(model.ToMainMenuItem());
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void UpdateMainMenuItem(EditMainMenuItemViewModel model)
        {
            _mainMenuItemService.UpdateMainMenuItem(model.ToMainMenuItem());
        }

        [HttpPost]
        public void DeleteMainMenuItem(int id)
        {
            _mainMenuItemService.DeleteMainMenuItem(id);
        }
    }
}