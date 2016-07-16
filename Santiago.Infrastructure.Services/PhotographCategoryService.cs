using System.Collections.Generic;
using System.Linq;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Repositories;
using Santiago.Core.Interfaces.Services;

namespace Santiago.Infrastructure.Services
{
    public class PhotographCategoryService : IPhotographCategoryService
    {
        private readonly IPhotographCategoryRepository _photographCategoryRepository;

        public PhotographCategoryService(IPhotographCategoryRepository photographCategoryRepository)
        {
            _photographCategoryRepository = photographCategoryRepository;
        }

        public PhotographCategory GetPhotographCategoryById(int id)
        {
            return _photographCategoryRepository.GetPhotographCategoryById(id);
        }

        public PhotographCategory GetPhotographCategoryByAlias(string alias)
        {
            return _photographCategoryRepository.GetPhotographCategoryByAlias(alias);
        }

        public List<PhotographCategory> GetAllPhotographCategoriesOrderedByOrderAsc()
        {
            return _photographCategoryRepository.GetAllPhotographCategoriesOrderedByOrderAsc().ToList();
        }

        public List<PhotographCategory> GetPhotographCategoriesOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            return _photographCategoryRepository.GetPhotographCategoriesOrderedByCreationDateDesc(skipNumber, takeNumber).ToList();
        }

        public int GetPhotographCategoriesTotalCount()
        {
            return _photographCategoryRepository.GetPhotographCategoriesTotalCount();
        }

        public int CreatePhotographCategory(PhotographCategory photographCategory)
        {
            return _photographCategoryRepository.CreatePhotographCategory(photographCategory);
        }

        public void UpdatePhotographCategory(PhotographCategory photographCategory)
        {
            _photographCategoryRepository.UpdatePhotographCategory(photographCategory);
        }

        public void DeletePhotographCategory(int id)
        {
            _photographCategoryRepository.DeletePhotographCategory(id);
        }
    }
}