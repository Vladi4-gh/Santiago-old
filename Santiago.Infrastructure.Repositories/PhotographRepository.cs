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
    public class PhotographRepository : IPhotographRepository
    {
        public Photograph GetPhotographById(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPhotograph>()
                    .Include(x => x.GalleryItemImageFile)
                    .Include(x => x.GallerySliderImageFile)
                    .SingleOrDefault(x => x.Id == id)
                    .ToPhotograph();
            }
        }

        public IEnumerable<Photograph> GetPhotographsOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPhotograph>()
                    .Include(x => x.GalleryItemImageFile)
                    .Include(x => x.GallerySliderImageFile)
                    .OrderByDescending(x => x.CreationDate)
                    .Skip(skipNumber)
                    .Take(takeNumber)
                    .ToList()
                    .Select(x => x.ToPhotograph());
            }
        }

        public IEnumerable<Photograph> GetPhotographsByCategoryAliasOrderedByCreationDateDesc(string categoryAlias, int skipNumber, int takeNumber)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPhotograph>()
                    .Include(x => x.GalleryItemImageFile)
                    .Include(x => x.GallerySliderImageFile)
                    .Where(x => x.Category != null && x.Category.Alias == categoryAlias)
                    .OrderByDescending(x => x.CreationDate)
                    .Skip(skipNumber)
                    .Take(takeNumber)
                    .ToList()
                    .Select(x => x.ToPhotograph());
            }
        }

        public int GetPhotographsTotalCount()
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPhotograph>()
                    .Count();
            }
        }

        public int CreatePhotograph(Photograph photograph)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbPhotograph = new DbPhotograph
                {
                    GalleryItemImageFileId = photograph.GalleryItemImageFileId,
                    GallerySliderImageFileId = photograph.GallerySliderImageFileId,
                    CategoryId = photograph.CategoryId,
                    Title = photograph.Title,
                    Description = photograph.Description,
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                };

                context
                    .Set<DbPhotograph>()
                    .Add(dbPhotograph);

                context.SaveChanges();

                return dbPhotograph.Id;
            }
        }

        public void UpdatePhotograph(Photograph photograph)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var selectedDbPhotograph = context
                    .Set<DbPhotograph>()
                    .SingleOrDefault(x => x.Id == photograph.Id);

                if (selectedDbPhotograph != null)
                {
                    selectedDbPhotograph.GalleryItemImageFileId = photograph.GalleryItemImageFileId;
                    selectedDbPhotograph.GallerySliderImageFileId = photograph.GallerySliderImageFileId;
                    selectedDbPhotograph.CategoryId = photograph.CategoryId;
                    selectedDbPhotograph.Title = photograph.Title;
                    selectedDbPhotograph.Description = photograph.Description;
                    selectedDbPhotograph.LastModifiedDate = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }

        public void DeletePhotograph(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbPhotograph = new DbPhotograph
                {
                    Id = id
                };

                context.Entry(dbPhotograph).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }
    }
}