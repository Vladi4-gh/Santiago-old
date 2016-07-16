using System.Collections.Generic;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Services
{
    public interface IPageTemplateService
    {
        List<PageTemplate> GetAllPageTemplates();
    }
}