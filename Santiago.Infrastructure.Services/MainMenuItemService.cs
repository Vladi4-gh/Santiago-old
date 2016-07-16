using System.Collections.Generic;
using System.Linq;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Repositories;
using Santiago.Core.Interfaces.Services;

namespace Santiago.Infrastructure.Services
{
    public class MainMenuItemService : IMainMenuItemService
    {
        private readonly IMainMenuItemRepository _mainMenuItemRepository;

        public MainMenuItemService(IMainMenuItemRepository mainMenuItemRepository)
        {
            _mainMenuItemRepository = mainMenuItemRepository;
        }

        public MainMenuItem GetMainMenuItemById(int id)
        {
            return _mainMenuItemRepository.GetMainMenuItemById(id);
        }

        public List<MainMenuItem> GetAllMainMenuItemsOrderedByOrderAsc()
        {
            return _mainMenuItemRepository.GetAllMainMenuItemsOrderedByOrderAsc().ToList();
        }

        public List<MainMenuItem> GetMainMenuItemsOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            return _mainMenuItemRepository.GetMainMenuItemsOrderedByCreationDateDesc(skipNumber, takeNumber).ToList();
        }

        public int GetMainMenuItemsTotalCount()
        {
            return _mainMenuItemRepository.GetMainMenuItemsTotalCount();
        }

        public int CreateMainMenuItem(MainMenuItem mainMenuItem)
        {
            return _mainMenuItemRepository.CreateMainMenuItem(mainMenuItem);
        }

        public void UpdateMainMenuItem(MainMenuItem mainMenuItem)
        {
            _mainMenuItemRepository.UpdateMainMenuItem(mainMenuItem);
        }

        public void DeleteMainMenuItem(int id)
        {
            _mainMenuItemRepository.DeleteMainMenuItem(id);
        }
    }
}