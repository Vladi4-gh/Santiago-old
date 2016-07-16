using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Repositories;
using Santiago.Infrastructure.Repositories.DataProviding;
using Santiago.Infrastructure.Repositories.DataProviding.DbEntities;
using Santiago.Infrastructure.Repositories.Helpers;

namespace Santiago.Infrastructure.Repositories
{
    public class PageRepository : IPageRepository
    {
        public Page GetPageById(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPage>()
                    .Include(x => x.Template)
                    .SingleOrDefault(x => x.Id == id)
                    .ToPage();
            }
        }

        public Page GetPublishedPageById(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPage>()
                    .Include(x => x.Template)
                    .SingleOrDefault(x => x.Id == id && x.IsPublished)
                    .ToPage();
            }
        }

        public Page GetPublishedPageByUrlPathAliases(string[] aliases)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbPagesSet = context
                    .Set<DbPage>()
                    .Include(x => x.Template);

                DbPage dbPage = null;
                int? parentId = null;

                foreach (var alias in aliases)
                {
                    dbPage = dbPagesSet.FirstOrDefault(x => x.Alias == alias && x.ParentId == parentId && x.IsPublished);

                    if (dbPage != null)
                    {
                        parentId = dbPage.Id;
                    }
                    else
                    {
                        break;
                    }
                }

                return dbPage.ToPage();
            }
        }

        public IEnumerable<Page> GetAllPublishedPagesOrderedByCreationDateDesc()
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPage>()
                    .Include(x => x.Parent)
                    .OrderByDescending(x => x.CreationDate)
                    .ToList()
                    .Select(x => x.ToPage());
            }
        }

        public IEnumerable<Page> GetPagesOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPage>()
                    .Include(x => x.Parent)
                    .OrderByDescending(x => x.CreationDate)
                    .Skip(skipNumber)
                    .Take(takeNumber)
                    .ToList()
                    .Select(x => x.ToPage());
            }
        }

        public int GetPagesTotalCount()
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPage>()
                    .Count();
            }
        }

        public int CreatePage(Page page)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbPage = new DbPage
                {
                    Title = page.Title,
                    Alias = page.Alias,
                    Text = page.Text,
                    MetaDescription = page.MetaDescription,
                    MetaKeywords = page.MetaKeywords,
                    ParentId = page.ParentId,
                    SitemapXmlChangeFrequency = page.SitemapXmlChangeFrequency,
                    SitemapXmlPriority = page.SitemapXmlPriority,
                    TemplateId = page.TemplateId,
                    IsPublished = page.IsPublished,
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    PublicationDate = page.IsPublished ? (DateTime?)DateTime.Now : null
                };

                context
                    .Set<DbPage>()
                    .Add(dbPage);

                context.SaveChanges();

                return dbPage.Id;
            }
        }

        public void UpdatePage(Page page)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var selectedDbPage = context
                    .Set<DbPage>()
                    .SingleOrDefault(x => x.Id == page.Id);

                if (selectedDbPage != null)
                {
                    selectedDbPage.Title = page.Title;
                    selectedDbPage.Alias = page.Alias;
                    selectedDbPage.Text = page.Text;
                    selectedDbPage.MetaDescription = page.MetaDescription;
                    selectedDbPage.MetaKeywords = page.MetaKeywords;
                    selectedDbPage.ParentId = page.ParentId;
                    selectedDbPage.SitemapXmlChangeFrequency = page.SitemapXmlChangeFrequency;
                    selectedDbPage.SitemapXmlPriority = page.SitemapXmlPriority;
                    selectedDbPage.TemplateId = page.TemplateId;
                    selectedDbPage.LastModifiedDate = DateTime.Now;

                    if (selectedDbPage.IsPublished != page.IsPublished)
                    {
                        selectedDbPage.IsPublished = page.IsPublished;
                        selectedDbPage.PublicationDate = page.IsPublished ? (DateTime?)DateTime.Now : null;
                    }

                    context.SaveChanges();
                }
            }
        }

        public void DeletePage(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbPagesSet = context.Set<DbPage>();
                var selectedDbPage = dbPagesSet.SingleOrDefault(x => x.Id == id);

                if (selectedDbPage != null)
                {
                    foreach (var childPage in dbPagesSet.Where(x => x.ParentId == selectedDbPage.Id))
                    {
                        childPage.ParentId = selectedDbPage.ParentId;
                    }

                    dbPagesSet.Remove(selectedDbPage);

                    context.SaveChanges();
                }
            }
        }
    }
}