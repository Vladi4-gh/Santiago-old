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
    public class MainMenuItemRepository : IMainMenuItemRepository
    {
        public MainMenuItem GetMainMenuItemById(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbMainMenuItem>()
                    .SingleOrDefault(x => x.Id == id)
                    .ToMainMenuItem();
            }
        }

        public IEnumerable<MainMenuItem> GetAllMainMenuItemsOrderedByOrderAsc()
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbMainMenuItem>()
                    .OrderBy(x => x.Order)
                    .ToList()
                    .Select(x => x.ToMainMenuItem());
            }
        }

        public IEnumerable<MainMenuItem> GetMainMenuItemsOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbMainMenuItem>()
                    .OrderByDescending(x => x.CreationDate)
                    .Skip(skipNumber)
                    .Take(takeNumber)
                    .ToList()
                    .Select(x => x.ToMainMenuItem());
            }
        }

        public int GetMainMenuItemsTotalCount()
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbMainMenuItem>()
                    .Count();
            }
        }

        public int CreateMainMenuItem(MainMenuItem mainMenuItem)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbMainMenuItem = new DbMainMenuItem
                {
                    Text = mainMenuItem.Text,
                    Url = mainMenuItem.Url,
                    Order = mainMenuItem.Order,
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                };

                context
                    .Set<DbMainMenuItem>()
                    .Add(dbMainMenuItem);

                context.SaveChanges();

                return dbMainMenuItem.Id;
            }
        }

        public void UpdateMainMenuItem(MainMenuItem mainMenuItem)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var selectedDbMainMenuItem = context
                    .Set<DbMainMenuItem>()
                    .SingleOrDefault(x => x.Id == mainMenuItem.Id);

                if (selectedDbMainMenuItem != null)
                {
                    selectedDbMainMenuItem.Text = mainMenuItem.Text;
                    selectedDbMainMenuItem.Url = mainMenuItem.Url;
                    selectedDbMainMenuItem.Order = mainMenuItem.Order;
                    selectedDbMainMenuItem.LastModifiedDate = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteMainMenuItem(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbMainMenuItem = new DbMainMenuItem
                {
                    Id = id
                };

                context.Entry(dbMainMenuItem).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }
    }
}