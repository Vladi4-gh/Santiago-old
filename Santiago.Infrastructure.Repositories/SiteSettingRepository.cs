using System.Collections.Generic;
using System.Linq;
using Santiago.Core.Entities;
using Santiago.Infrastructure.Repositories.DataProviding;
using Santiago.Infrastructure.Repositories.DataProviding.DbEntities;
using Santiago.Infrastructure.Repositories.Helpers;

namespace Santiago.Infrastructure.Repositories
{
    public class SiteSettingRepository
    {
        public IEnumerable<SiteSetting> GetAllSiteSettings()
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbSiteSetting>()
                    .ToList()
                    .Select(x => x.ToSiteSetting());
            }
        }

        public void UpdateSiteSetting(SiteSetting siteSetting)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var selectedDbSiteSetting = context
                    .Set<DbSiteSetting>()
                    .SingleOrDefault(x => x.Id == siteSetting.Id);

                if (selectedDbSiteSetting != null)
                {
                    selectedDbSiteSetting.Value = siteSetting.Value;

                    context.SaveChanges();
                }
            }
        }

        public void UpdateSiteSettings(List<SiteSetting> siteSettings)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var siteSettingsToUpdateNames = siteSettings.Select(x => x.Name);
                var siteSettingsToUpdate = context
                    .Set<DbSiteSetting>()
                    .Where(x => siteSettingsToUpdateNames.Contains(x.Name))
                    .ToArray();

                foreach (var siteSettingToUpdate in siteSettingsToUpdate)
                {
                    siteSettingToUpdate.Value = siteSettings.Single(x => x.Name == siteSettingToUpdate.Name).Value;
                }

                context.SaveChanges();
            }
        }
    }
}