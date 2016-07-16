using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Santiago.Core.Interfaces.Services;
using Santiago.Infrastructure.DependencyResolution;

namespace Santiago.Web.Handlers.MvcRouteHandlers
{
    public class PageRouteHandler : MvcRouteHandler
    {
        private readonly IPageService _pageService;

        public PageRouteHandler()
        {
            _pageService = AutofacConfig.Container.Resolve<IPageService>();
        }

        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var page = _pageService.GetPublishedPageByUrlPath(requestContext.HttpContext.Request.Path);

            if (page != null)
            {
                requestContext.RouteData.Values["controller"] = page.Template.ControllerName;
                requestContext.RouteData.Values["action"] = page.Template.ActionName;
                requestContext.RouteData.Values["page"] = page;
            }

            if (requestContext.RouteData.Values["controller"] == null || requestContext.RouteData.Values["action"] == null)
            {
                throw new HttpException(404, "Not Found");
            }

            return base.GetHttpHandler(requestContext);
        }
    }
}