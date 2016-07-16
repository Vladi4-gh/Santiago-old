using System.Collections.Generic;
using System.Linq;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Repositories;
using Santiago.Core.Interfaces.Services;

namespace Santiago.Infrastructure.Services
{
    public class PageTemplateService : IPageTemplateService
    {
        private readonly IPageTemplateRepository _pageTemplateRepository;

        public PageTemplateService(IPageTemplateRepository pageTemplateRepository)
        {
            _pageTemplateRepository = pageTemplateRepository;
        }

        public List<PageTemplate> GetAllPageTemplates()
        {
            return _pageTemplateRepository.GetAllPageTemplates().ToList();
        }
    }
}