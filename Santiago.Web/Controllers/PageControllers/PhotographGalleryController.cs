using System.Web.Mvc;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Services;
using Santiago.Web.Controllers.BaseControllers;
using Santiago.Web.Helpers;
using Santiago.Web.ViewModels.PhotographGallery;

namespace Santiago.Web.Controllers.PageControllers
{
    public class PhotographGalleryController : PageBaseController
    {
        private readonly IPhotographService _photographService;

        private readonly IPhotographCategoryService _photographCategoryService;

        public PhotographGalleryController(IPhotographService photographService, IPhotographCategoryService photographCategoryService)
        {
            _photographService = photographService;
            _photographCategoryService = photographCategoryService;
        }

        [HttpGet]
        public ActionResult Index(Page page)
        {
            ViewBag.ShowScrollBar = true;
            ViewBag.MetaDescription = page.MetaDescription;
            ViewBag.MetaKeywords = page.MetaKeywords;
            ViewBag.PageTitle = page.Title;

            var photographCategories = _photographCategoryService.GetAllPhotographCategoriesOrderedByOrderAsc().ToViewModelList(x => x.ToPhotographCategoryViewModel());

            photographCategories.Insert(0, new PhotographCategoryViewModel
            {
                Name = "Все фотографии",
                Alias = "all"
            });

            ViewBag.PhotographCategories = photographCategories;

            return View();
        }

        [HttpGet]
        public JsonResult GetPhotographsByCategoryAliasOrderedByCreationDateDesc(string categoryAlias, int skipNumber, int takeNumber)
        {
            var model = categoryAlias == "all"
                ? _photographService.GetPhotographsOrderedByCreationDateDesc(skipNumber, takeNumber).ToViewModelList(x => x.ToPhotographViewModel())
                : _photographService.GetPhotographsByCategoryAliasOrderedByCreationDateDesc(categoryAlias, skipNumber, takeNumber).ToViewModelList(x => x.ToPhotographViewModel());

            return Json(SerializeObjectToJson(model), JsonRequestBehavior.AllowGet);
        }
    }
}