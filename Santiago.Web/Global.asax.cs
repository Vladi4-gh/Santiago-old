using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Santiago.Web.Controllers;

namespace Santiago.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ContainerConfig.Register();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            Server.ClearError();

            var httpException = exception as HttpException;
            var routeData = new RouteData();

            routeData.Values["controller"] = "Error";

            if (httpException == null)
            {
                routeData.Values["action"] = "InternalServerError";
                routeData.Values["exception"] = exception;
            }
            else
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        routeData.Values["action"] = "NotFound";

                        break;
                    case 500:
                        routeData.Values["action"] = "InternalServerError";
                        routeData.Values["exception"] = httpException;

                        break;
                    default:
                        routeData.Values["action"] = "InternalServerError";
                        routeData.Values["exception"] = httpException;

                        break;
                }
            }

            Response.TrySkipIisCustomErrors = true;

            IController errorController = new ErrorController();

            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));

            Response.End();
        }
    }
}