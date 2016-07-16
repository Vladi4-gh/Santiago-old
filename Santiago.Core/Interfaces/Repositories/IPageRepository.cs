using System.Collections.Generic;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Repositories
{
    public interface IPageRepository
    {
        Page GetPageById(int id);

        Page GetPublishedPageById(int id);

        Page GetPublishedPageByUrlPathAliases(string[] aliases);

        IEnumerable<Page> GetAllPublishedPagesOrderedByCreationDateDesc();

        IEnumerable<Page> GetPagesOrderedByCreationDateDesc(int skipNumber, int takeNumber);

        int GetPagesTotalCount();

        int CreatePage(Page page);

        void UpdatePage(Page page);

        void DeletePage(int id);
    }
}