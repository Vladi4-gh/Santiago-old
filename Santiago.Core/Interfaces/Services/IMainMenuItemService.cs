using System.Collections.Generic;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Services
{
    public interface IMainMenuItemService
    {
        MainMenuItem GetMainMenuItemById(int id);

        List<MainMenuItem> GetAllMainMenuItemsOrderedByOrderAsc();

        List<MainMenuItem> GetMainMenuItemsOrderedByCreationDateDesc(int skipNumber, int takeNumber);

        int GetMainMenuItemsTotalCount();

        int CreateMainMenuItem(MainMenuItem mainMenuItem);

        void UpdateMainMenuItem(MainMenuItem mainMenuItem);

        void DeleteMainMenuItem(int id);
    }
}