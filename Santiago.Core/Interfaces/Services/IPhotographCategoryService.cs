using System.Collections.Generic;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Services
{
    public interface IPhotographCategoryService
    {
        PhotographCategory GetPhotographCategoryById(int id);

        PhotographCategory GetPhotographCategoryByAlias(string alias);

        List<PhotographCategory> GetAllPhotographCategoriesOrderedByOrderAsc();

        List<PhotographCategory> GetPhotographCategoriesOrderedByCreationDateDesc(int skipNumber, int takeNumber);

        int GetPhotographCategoriesTotalCount();

        int CreatePhotographCategory(PhotographCategory photographCategory);

        void UpdatePhotographCategory(PhotographCategory photographCategory);

        void DeletePhotographCategory(int id);
    }
}