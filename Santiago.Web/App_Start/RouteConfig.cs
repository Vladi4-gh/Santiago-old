using System.Web.Mvc;
using System.Web.Routing;
using Santiago.Web.Handlers.MvcRouteHandlers;
using PageRouteHandler = Santiago.Web.Handlers.MvcRouteHandlers.PageRouteHandler;

namespace Santiago.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = true;
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("bundles/{*pathInfo}");
            routes.IgnoreRoute("robots.txt");
            routes.IgnoreRoute("sitemap.xml");

            routes.MapRoute(
                "FileUpload",
                "FileUpload/{action}",
                new
                {
                    controller = "FileUpload"
                },
                new[]
                {
                    "Santiago.Web.Controllers"
                });

            routes.MapRoute(
                "Authorization",
                "Authorization/{action}",
                new
                {
                    controller = "Authorization"
                },
                new[]
                {
                    "Santiago.Web.Controllers"
                });

            routes.MapRoute(
                "Api",
                "api/{*path}",
                new[]
                {
                    "Santiago.Web.Controllers.PageControllers"
                })
                .RouteHandler = new ApiRouteHandler();

            routes.MapRoute(
                "Page",
                "{*path}",
                new[]
                {
                    "Santiago.Web.Controllers.PageControllers"
                })
                .RouteHandler = new PageRouteHandler();
        }
    }
}