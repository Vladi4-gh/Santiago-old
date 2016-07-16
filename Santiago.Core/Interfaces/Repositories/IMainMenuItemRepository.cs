using System.Collections.Generic;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Repositories
{
    public interface IMainMenuItemRepository
    {
        MainMenuItem GetMainMenuItemById(int id);

        IEnumerable<MainMenuItem> GetAllMainMenuItemsOrderedByOrderAsc();

        IEnumerable<MainMenuItem> GetMainMenuItemsOrderedByCreationDateDesc(int skipNumber, int takeNumber);

        int GetMainMenuItemsTotalCount();

        int CreateMainMenuItem(MainMenuItem mainMenuItem);

        void UpdateMainMenuItem(MainMenuItem mainMenuItem);

        void DeleteMainMenuItem(int id);
    }
}