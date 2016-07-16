using System;
using System.Web.Mvc;
using Autofac;
using Santiago.Core.Interfaces.Logging;
using Santiago.Infrastructure.DependencyResolution;
using Santiago.Web.Controllers.BaseControllers;

namespace Santiago.Web.Controllers
{
    public class ErrorController : BaseController
    {
        private static ILogger Logger
        {
            get
            {
                return AutofacConfig.Container.Resolve<ILogger>();
            }
        }

        public ErrorController()
        {
            ViewBag.LayoutName = "Error";
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult NotFound()
        {
            ViewBag.PageTitle = "404 - Страница не найдена";

            Response.StatusCode = 404;

            return MakeErrorResponse(null, "404 Not Found");
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult InternalServerError(Exception exception)
        {
            ViewBag.PageTitle = "500 - Произошла ошибка!";
            ViewBag.ShowException = Properties.Settings.Default.Environment == "Dev" || Properties.Settings.Default.Environment == "Test";

            Response.StatusCode = 500;

            Logger.Error(exception, exception.Message);

            return MakeErrorResponse(exception, "500 Internal Server Error");
        }

        private ActionResult MakeErrorResponse(object model, string ajaxResponseMessage)
        {
            ActionResult result;

            if (!Request.IsAjaxRequest())
            {
                result = View(model);
            }
            else
            {
                result = Content(ajaxResponseMessage);
            }

            return result;
        }
    }
}