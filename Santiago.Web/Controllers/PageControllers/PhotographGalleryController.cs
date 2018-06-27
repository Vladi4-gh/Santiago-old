using System.Web.Mvc;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Services;
using Santiago.Web.Controllers.BaseControllers;
using Santiago.Web.Helpers;
using Santiago.Web.ViewModels.PhotographGallery;

namespace Santiago.Web.Controllers.PageControllers
{
    public class SantiagoWebPhotographGalleryController : PageBaseController
    {
        private readonly IPhotographService _santiagoWebPhotographGalleryControllerPhotographService;

        private readonly IPhotographCategoryService _santiagoWebPhotographGalleryControllerPhotographCategoryService;

        public SantiagoWebPhotographGalleryController(IPhotographService santiagoWebPhotographGalleryControllerPhotographService, IPhotographCategoryService santiagoWebPhotographGalleryControllerPhotographCategoryService)
        {
            _santiagoWebPhotographGalleryControllerPhotographService = santiagoWebPhotographGalleryControllerPhotographService;
            _santiagoWebPhotographGalleryControllerPhotographCategoryService = santiagoWebPhotographGalleryControllerPhotographCategoryService;
        }

        [HttpGet]
        public ActionResult Index(Page page)
        {
            ViewBag.ShowScrollBar = true;
            ViewBag.MetaDescription = page.MetaDescription;
            ViewBag.MetaKeywords = page.MetaKeywords;
            ViewBag.PageTitle = page.Title;

            var santiagoWebPhotographGalleryControllerPhotographCategories = _santiagoWebPhotographGalleryControllerPhotographCategoryService.GetAllPhotographCategoriesOrderedByOrderAsc().ToViewModelList(x => x.ToPhotographCategoryViewModel());

            santiagoWebPhotographGalleryControllerPhotographCategories.Insert(0, new PhotographCategoryViewModel
            {
                Name = "Все фотографии",
                Alias = "all"
            });

            ViewBag.PhotographCategories = santiagoWebPhotographGalleryControllerPhotographCategories;

            return View();
        }

        [HttpGet]
        public JsonResult GetPhotographsByCategoryAliasOrderedByCreationDateDesc(string santiagoWebPhotographGalleryControllerCategoryAlias, int santiagoWebPhotographGalleryControllerSkipNumber, int santiagoWebPhotographGalleryControllerTakeNumber)
        {
            var santiagoWebPhotographGalleryControllerModel = santiagoWebPhotographGalleryControllerCategoryAlias == "all"
                ? _santiagoWebPhotographGalleryControllerPhotographService.GetPhotographsOrderedByCreationDateDesc(santiagoWebPhotographGalleryControllerSkipNumber, santiagoWebPhotographGalleryControllerTakeNumber).ToViewModelList(x => x.ToPhotographViewModel())
                : _santiagoWebPhotographGalleryControllerPhotographService.GetPhotographsByCategoryAliasOrderedByCreationDateDesc(santiagoWebPhotographGalleryControllerCategoryAlias, santiagoWebPhotographGalleryControllerSkipNumber, santiagoWebPhotographGalleryControllerTakeNumber).ToViewModelList(santiagoWebPhotographGalleryControllerX => santiagoWebPhotographGalleryControllerX.ToPhotographViewModel());

            return Json(SerializeObjectToJson(santiagoWebPhotographGalleryControllerModel), JsonRequestBehavior.AllowGet);
        }
    }
}