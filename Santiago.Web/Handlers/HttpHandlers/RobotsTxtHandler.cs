using System;
using System.Web;
using Santiago.Infrastructure.Services;

namespace Santiago.Web.Handlers.HttpHandlers
{
    public class RobotsTxtHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var siteUrl = SiteSettingService.SiteUrl;

            context.Response.ContentType = "text/plain";
            context.Response.Write(
                "User-agent: *\n" +
                "Disallow: /admin\n" +
                "\n" +
                "Sitemap: " + (new Uri(new Uri(siteUrl), "sitemap.xml")) + "\n" +
                "\n" +
                "Host: " + (new Uri(siteUrl)).Host);
        }
    }
}