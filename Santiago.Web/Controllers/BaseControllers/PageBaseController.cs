using Autofac;
using Santiago.Core.Interfaces.Services;
using Santiago.Infrastructure.DependencyResolution;
using Santiago.Web.Helpers;

namespace Santiago.Web.Controllers.BaseControllers
{
    public abstract class PageBaseController : BaseController
    {
        protected PageBaseController()
        {
            ViewBag.LayoutName = "Page";
            ViewBag.MainMenuItems = AutofacConfig.Container.Resolve<IMainMenuItemService>().GetAllMainMenuItemsOrderedByOrderAsc().ToViewModelList(x => x.ToMainMenuItemViewModel());
        }
    }
}