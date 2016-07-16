using System.ComponentModel.DataAnnotations;

namespace Santiago.Web.Areas.Admin.ViewModels.SiteSetting
{
    public class EditSiteSettingsViewModel
    {
        [Display(Name = "Главная страница")]
        public int? MainPageId { get; set; }

        public string MainPageTitle { get; set; }
    }
}