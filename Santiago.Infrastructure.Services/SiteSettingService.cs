using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Santiago.Core.Entities;
using Santiago.Infrastructure.Repositories;

namespace Santiago.Infrastructure.Services
{
    public static class SiteSettingService
    {
        private static readonly MemoryCache _siteSettingsMemoryCache = new MemoryCache("SiteSettingsMemoryCache");

        #region Основные настройки

        public static string SiteUrl
        {
            get
            {
                return "http://sashasantiago.ru";
            }
            set
            {
                
            }
        }

        public static int? MainPageId
        {
            get
            {
                var mainPageId = GetSiteSettingValueByName("MainPageId");

                return mainPageId == null ? (int?)null : int.Parse(mainPageId);
            }
            set
            {
                UpdateSiteSetting("MainPageId", value == null ? null : value.ToString());
            }
        }

        #endregion

        private static SiteSettingRepository GetNewSiteSettingRepository()
        {
            return new SiteSettingRepository();
        }

        private static SiteSetting GetSiteSettingByName(string name)
        {
            var isSiteSettingExists = _siteSettingsMemoryCache.Contains(name);

            if (!isSiteSettingExists)
            {
                foreach (var siteSetting in GetNewSiteSettingRepository().GetAllSiteSettings())
                {
                    _siteSettingsMemoryCache.Add(siteSetting.Name, siteSetting, DateTimeOffset.MaxValue);
                }
            }

            return (SiteSetting)_siteSettingsMemoryCache.Get(name);
        }

        private static string GetSiteSettingValueByName(string name)
        {
            return GetSiteSettingByName(name).Value;
        }

        private static void UpdateSiteSetting(string name, string value, bool needToUpdateInDatabase = true)
        {
            var siteSettingFromMemoryCache = GetSiteSettingByName(name);

            siteSettingFromMemoryCache.Value = value;

            if (needToUpdateInDatabase)
            {
                GetNewSiteSettingRepository().UpdateSiteSetting(siteSettingFromMemoryCache);
            }

            _siteSettingsMemoryCache.Set(name, siteSettingFromMemoryCache, DateTimeOffset.MaxValue);
        }

        public static void UpdateSiteSettings(List<SiteSetting> siteSettings)
        {
            GetNewSiteSettingRepository().UpdateSiteSettings(siteSettings);

            foreach (var siteSetting in siteSettings)
            {
                UpdateSiteSetting(siteSetting.Name, siteSetting.Value, false);
            }
        }
    }
}