using System.Collections.Generic;
using System.Web.Mvc;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Services;
using Santiago.Infrastructure.Services;
using Santiago.Web.Areas.Admin.ViewModels.SiteSetting;
using Santiago.Web.Attributes.ActionFilterAttributes;
using Santiago.Web.Controllers.BaseControllers;

namespace Santiago.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class SiteSettingController : BaseController
    {
        private readonly IPageService _pageService;

        public SiteSettingController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Настройки сайта";

            return View(new EditSiteSettingsViewModel
            {
                MainPageId = SiteSettingService.MainPageId,
                MainPageTitle = GetMainPageTitle()
            });
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void UpdateSiteSettings(EditSiteSettingsViewModel model)
        {
            var siteSettingsToUpdate = new List<SiteSetting>();

            if (SiteSettingService.MainPageId != model.MainPageId)
            {
                //SiteSettingService.MainPageId = model.MainPageId;
                //siteSettingsToUpdate.Add(SiteSettingService.MainPageIdSiteSetting);

                
            }

            if (siteSettingsToUpdate.Count != 0)
            {
                SiteSettingService.UpdateSiteSettings(siteSettingsToUpdate);
            }
        }

        private string GetMainPageTitle()
        {
            var mainPageTitle = string.Empty;

            if (SiteSettingService.MainPageId.HasValue)
            {
                var mainPage = _pageService.GetPageById(SiteSettingService.MainPageId.Value);

                if (mainPage != null)
                {
                    mainPageTitle = mainPage.Title;
                }
            }

            return mainPageTitle;
        }
    }
}