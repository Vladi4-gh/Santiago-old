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
    public class PhotographCategoryRepository : IPhotographCategoryRepository
    {
        public PhotographCategory GetPhotographCategoryById(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPhotographCategory>()
                    .SingleOrDefault(x => x.Id == id)
                    .ToPhotographCategory();
            }
        }

        public PhotographCategory GetPhotographCategoryByAlias(string alias)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPhotographCategory>()
                    .SingleOrDefault(x => x.Alias == alias)
                    .ToPhotographCategory();
            }
        }

        public IEnumerable<PhotographCategory> GetAllPhotographCategoriesOrderedByOrderAsc()
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPhotographCategory>()
                    .OrderBy(x => x.Order)
                    .ToList()
                    .Select(x => x.ToPhotographCategory());
            }
        }

        public IEnumerable<PhotographCategory> GetPhotographCategoriesOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPhotographCategory>()
                    .OrderByDescending(x => x.CreationDate)
                    .Skip(skipNumber)
                    .Take(takeNumber)
                    .ToList()
                    .Select(x => x.ToPhotographCategory());
            }
        }

        public int GetPhotographCategoriesTotalCount()
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPhotographCategory>()
                    .Count();
            }
        }

        public int CreatePhotographCategory(PhotographCategory photographCategory)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbPhotographCategory = new DbPhotographCategory
                {
                    Name = photographCategory.Name,
                    Alias = photographCategory.Alias,
                    Order = photographCategory.Order,
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                };

                context
                    .Set<DbPhotographCategory>()
                    .Add(dbPhotographCategory);

                context.SaveChanges();

                return dbPhotographCategory.Id;
            }
        }

        public void UpdatePhotographCategory(PhotographCategory photographCategory)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var selectedDbPhotographCategory = context
                    .Set<DbPhotographCategory>()
                    .SingleOrDefault(x => x.Id == photographCategory.Id);

                if (selectedDbPhotographCategory != null)
                {
                    selectedDbPhotographCategory.Name = photographCategory.Name;
                    selectedDbPhotographCategory.Alias = photographCategory.Alias;
                    selectedDbPhotographCategory.Order = photographCategory.Order;
                    selectedDbPhotographCategory.LastModifiedDate = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }

        public void DeletePhotographCategory(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbPhotographCategory = new DbPhotographCategory
                {
                    Id = id
                };

                context.Entry(dbPhotographCategory).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }
    }
}