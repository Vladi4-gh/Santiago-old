using System.Web.Mvc;
using Santiago.Core.Interfaces.Services;
using Santiago.Web.Areas.Admin.ViewModels.Common;
using Santiago.Web.Areas.Admin.ViewModels.Page;
using Santiago.Web.Attributes.ActionFilterAttributes;
using Santiago.Web.Controllers.BaseControllers;
using Santiago.Web.Helpers;

namespace Santiago.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class PageController : BaseController
    {
        private readonly IPageService _pageService;

        private readonly IPageTemplateService _pageTemplateService;

        public PageController(IPageService pageService, IPageTemplateService pageTemplateService)
        {
            _pageService = pageService;
            _pageTemplateService = pageTemplateService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Страницы";
            ViewBag.SitemapXmlChangeFrequenciesSelectListItems = new[]
            {
                new SelectListItemViewModel
                {
                    Value = null,
                    Text = "Не указывать"
                },
                new SelectListItemViewModel
                {
                    Value = "always",
                    Text = "Всегда (always)"
                },
                new SelectListItemViewModel
                {
                    Value = "hourly",
                    Text = "Ежечасно (hourly)"
                },
                new SelectListItemViewModel
                {
                    Value = "daily",
                    Text = "Ежедневно (daily)"
                },
                new SelectListItemViewModel
                {
                    Value = "weekly",
                    Text = "Еженедельно (weekly)"
                },
                new SelectListItemViewModel
                {
                    Value = "monthly",
                    Text = "Ежемесячно (monthly)"
                },
                new SelectListItemViewModel
                {
                    Value = "yearly",
                    Text = "Ежегодно (yearly)"
                },
                new SelectListItemViewModel
                {
                    Value = "never",
                    Text = "Никогда (never)"
                }
            };
            ViewBag.SitemapXmlPrioritiesSelectListItems = new[]
            {
                new SelectListItemViewModel
                {
                    Value = null,
                    Text = "Не указывать"
                },
                new SelectListItemViewModel
                {
                    Value = "0.0",
                    Text = "0.0"
                },
                new SelectListItemViewModel
                {
                    Value = "0.1",
                    Text = "0.1"
                },
                new SelectListItemViewModel
                {
                    Value = "0.2",
                    Text = "0.2"
                },
                new SelectListItemViewModel
                {
                    Value = "0.3",
                    Text = "0.3"
                },
                new SelectListItemViewModel
                {
                    Value = "0.4",
                    Text = "0.4"
                },
                new SelectListItemViewModel
                {
                    Value = "0.5",
                    Text = "0.5"
                },
                new SelectListItemViewModel
                {
                    Value = "0.6",
                    Text = "0.6"
                },
                new SelectListItemViewModel
                {
                    Value = "0.7",
                    Text = "0.7"
                },
                new SelectListItemViewModel
                {
                    Value = "0.8",
                    Text = "0.8"
                },
                new SelectListItemViewModel
                {
                    Value = "0.9",
                    Text = "0.9"
                },
                new SelectListItemViewModel
                {
                    Value = "1.0",
                    Text = "1.0"
                }
            };
            ViewBag.PageTemplatesSelectListItems = _pageTemplateService.GetAllPageTemplates().ToViewModelList(x => x.ToSelectListItemViewModel());
            ViewBag.PublicationStatusesSelectListItems = new[]
            {
                new SelectListItemViewModel
                {
                    Value = "false",
                    Text = "Не опубликована"
                },
                new SelectListItemViewModel
                {
                    Value = "true",
                    Text = "Опубликована"
                }
            };

            return View();
        }

        [HttpGet]
        public JsonResult GetPagesOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            var pagesTotalCount = _pageService.GetPagesTotalCount();
            var model = new DataTableViewModel<int, PageDataTableItemViewModel>
            {
                ItemsTotalCount = pagesTotalCount,
                Items = _pageService.GetPagesOrderedByCreationDateDesc(DataTableHelper.GetRightSkipNumber(skipNumber, takeNumber, pagesTotalCount), takeNumber).ToViewModelList(x => x.ToPageDataTableItemViewModel())
            };

            return Json(SerializeObjectToJson(model), JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void CreatePage(AddPageViewModel model)
        {
            _pageService.CreatePage(model.ToPage());
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void UpdatePage(EditPageViewModel model)
        {
            _pageService.UpdatePage(model.ToPage());
        }

        [HttpPost]
        public void DeletePage(int id)
        {
            _pageService.DeletePage(id);
        }
    }
}