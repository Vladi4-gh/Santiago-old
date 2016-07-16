using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Autofac;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Services;
using Santiago.Infrastructure.DependencyResolution;
using Santiago.Infrastructure.Services;

namespace Santiago.Web.Handlers.HttpHandlers
{
    public class SitemapXmlHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var pages = AutofacConfig.Container.Resolve<IPageService>().GetAllPublishedPagesOrderedByCreationDateDesc();

            AddMainPageToPagesList(pages);

            XNamespace sitemapXmlNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";
            var sitemapXml = new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                new XElement(sitemapXmlNamespace + "urlset",
                    pages.Select(x =>
                        new XElement(sitemapXmlNamespace + "url",
                            new XElement(sitemapXmlNamespace + "loc", GetPageAbsoluteUrl(x)),
                            new XElement(sitemapXmlNamespace + "lastmod", x.LastModifiedDate.ToString("yyyy-MM-ddThh:mm:ss")),
                            x.SitemapXmlChangeFrequency == null ? null : new XElement(sitemapXmlNamespace + "changefreq", x.SitemapXmlChangeFrequency),
                            !x.SitemapXmlPriority.HasValue ? null : new XElement(sitemapXmlNamespace + "priority", x.SitemapXmlPriority.Value.ToString("0.0", CultureInfo.InvariantCulture))
                        )
                    )
                )
            );

            context.Response.ContentType = "text/xml";
            context.Response.Write(sitemapXml.Declaration + Environment.NewLine + sitemapXml);
        }

        private void AddMainPageToPagesList(List<Page> pagesList)
        {
            var mainPageId = SiteSettingService.MainPageId;

            if (mainPageId.HasValue)
            {
                var mainPage = pagesList.FirstOrDefault(x => x.Id == mainPageId);

                if (mainPage != null)
                {
                    pagesList.Insert(0, new Page
                    {
                        Alias = string.Empty,
                        LastModifiedDate = mainPage.LastModifiedDate,
                        SitemapXmlChangeFrequency = mainPage.SitemapXmlChangeFrequency,
                        SitemapXmlPriority = mainPage.SitemapXmlPriority
                    });
                }
            }
        }

        private string GetPageAbsoluteUrl(Page page)
        {
            var isPageAbsoluteUrlBuilt = false;
            var pageAbsoluteUrlAliases = new List<string>();
            var currentPage = page;

            do
            {
                pageAbsoluteUrlAliases.Add(currentPage.Alias);

                if (currentPage.ParentId.HasValue)
                {
                    currentPage = currentPage.Parent;
                }
                else
                {
                    isPageAbsoluteUrlBuilt = true;
                }
            } while (!isPageAbsoluteUrlBuilt);

            pageAbsoluteUrlAliases.Reverse();

            var pageAbsoluteUrl = new Uri(new Uri(SiteSettingService.SiteUrl), string.Join("/", pageAbsoluteUrlAliases)).ToString();

            if (pageAbsoluteUrl.EndsWith("/"))
            {
                pageAbsoluteUrl = pageAbsoluteUrl.Remove(pageAbsoluteUrl.Length - 1, 1);
            }

            return pageAbsoluteUrl;
        }
    }
}