using System.Collections.Generic;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Repositories
{
    public interface IPageTemplateRepository
    {
        IEnumerable<PageTemplate> GetAllPageTemplates();
    }
}