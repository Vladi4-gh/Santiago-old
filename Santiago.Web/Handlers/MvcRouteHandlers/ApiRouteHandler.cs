using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Santiago.Web.Handlers.MvcRouteHandlers
{
    public class ApiRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var apiUrl = requestContext.HttpContext.Request.Path.ToLower();

            if (apiUrl.StartsWith("/api"))
            {
                apiUrl = apiUrl.Remove(0, 4);
            }

            var apiUrlSegments = apiUrl.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            switch (apiUrlSegments.FirstOrDefault())
            {
                case "photographs":
                    switch (apiUrlSegments.Skip(1).FirstOrDefault())
                    {
                        case "getphotographsbycategoryaliasorderedbycreationdatedesc":
                            requestContext.RouteData.Values["controller"] = "PhotographGallery";
                            requestContext.RouteData.Values["action"] = "GetPhotographsByCategoryAliasOrderedByCreationDateDesc";

                            break;
                    }

                    break;
            }

            if (requestContext.RouteData.Values["controller"] == null || requestContext.RouteData.Values["action"] == null)
            {
                throw new HttpException(404, "Not Found");
            }

            return base.GetHttpHandler(requestContext);
        }
    }
}