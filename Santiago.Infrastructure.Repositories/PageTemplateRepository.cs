using System.Collections.Generic;
using System.Linq;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Repositories;
using Santiago.Infrastructure.Repositories.DataProviding;
using Santiago.Infrastructure.Repositories.DataProviding.DbEntities;
using Santiago.Infrastructure.Repositories.Helpers;

namespace Santiago.Infrastructure.Repositories
{
    public class PageTemplateRepository : IPageTemplateRepository
    {
        public IEnumerable<PageTemplate> GetAllPageTemplates()
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbPageTemplate>()
                    .ToList()
                    .Select(x => x.ToPageTemplate());
            }
        }
    }
}