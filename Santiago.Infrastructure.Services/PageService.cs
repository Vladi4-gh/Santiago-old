using System;
using System.Collections.Generic;
using System.Linq;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Repositories;
using Santiago.Core.Interfaces.Services;

namespace Santiago.Infrastructure.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;

        public PageService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public Page GetPageById(int id)
        {
            return _pageRepository.GetPageById(id);
        }

        public Page GetPublishedPageByUrlPath(string urlPath)
        {
            if (urlPath == "/")
            {
                return SiteSettingService.MainPageId.HasValue ? _pageRepository.GetPublishedPageById(SiteSettingService.MainPageId.Value) : null;
            }

            return _pageRepository.GetPublishedPageByUrlPathAliases(urlPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public List<Page> GetAllPublishedPagesOrderedByCreationDateDesc()
        {
            return _pageRepository.GetAllPublishedPagesOrderedByCreationDateDesc().ToList();
        }

        public List<Page> GetPagesOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            return _pageRepository.GetPagesOrderedByCreationDateDesc(skipNumber, takeNumber).ToList();
        }

        public int GetPagesTotalCount()
        {
            return _pageRepository.GetPagesTotalCount();
        }

        public int CreatePage(Page page)
        {
            return _pageRepository.CreatePage(page);
        }

        public void UpdatePage(Page page)
        {
            _pageRepository.UpdatePage(page);
        }

        public void DeletePage(int id)
        {
            _pageRepository.DeletePage(id);

            if (SiteSettingService.MainPageId == id)
            {
                SiteSettingService.MainPageId = null;
            }
        }
    }
}