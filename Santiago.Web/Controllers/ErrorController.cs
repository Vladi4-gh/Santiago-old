using System;
using System.Web.Mvc;
using Autofac;
using Santiago.Core.Interfaces.Logging;
using Santiago.Infrastructure.DependencyResolution;
using Santiago.Web.Controllers.BaseControllers;

namespace Santiago.Web.Controllers
{
    public class SantiagoWebErrorController : BaseController
    {
        private static ILogger SantiagoWebErrorControllerLogger
        {
            get
            {
                return AutofacConfig.Container.Resolve<ILogger>();
            }
        }

        public SantiagoWebErrorController()
        {
            ViewBag.LayoutName = "Error";
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult NotFound()
        {
            ViewBag.PageTitle = "404 - Страница не найдена";

            Response.StatusCode = 404;

            return SantiagoWebErrorControllerMakeErrorResponse(null, "404 Not Found");
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult InternalServerError(Exception santiagoWebErrorControllerException)
        {
            ViewBag.PageTitle = "500 - Произошла ошибка!";
            ViewBag.ShowException = Properties.Settings.Default.Environment == "Dev" || Properties.Settings.Default.Environment == "Test";

            Response.StatusCode = 500;

            SantiagoWebErrorControllerLogger.Error(santiagoWebErrorControllerException, santiagoWebErrorControllerException.Message);

            return SantiagoWebErrorControllerMakeErrorResponse(santiagoWebErrorControllerException, "500 Internal Server Error");
        }

        private ActionResult SantiagoWebErrorControllerMakeErrorResponse(object santiagoWebErrorControllerModel, string santiagoWebErrorControllerSjaxResponseMessage)
        {
            ActionResult santiagoWebErrorControllerResult;

            if (!Request.IsAjaxRequest())
            {
                santiagoWebErrorControllerResult = View(santiagoWebErrorControllerModel);
            }
            else
            {
                santiagoWebErrorControllerResult = Content(santiagoWebErrorControllerSjaxResponseMessage);
            }

            return santiagoWebErrorControllerResult;
        }
    }
}