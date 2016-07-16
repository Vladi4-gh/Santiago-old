using System.Collections.Generic;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Services
{
    public interface IPageService
    {
        Page GetPageById(int id);

        Page GetPublishedPageByUrlPath(string urlPath);

        List<Page> GetAllPublishedPagesOrderedByCreationDateDesc();

        List<Page> GetPagesOrderedByCreationDateDesc(int skipNumber, int takeNumber);

        int GetPagesTotalCount();

        int CreatePage(Page page);

        void UpdatePage(Page page);

        void DeletePage(int id);
    }
}