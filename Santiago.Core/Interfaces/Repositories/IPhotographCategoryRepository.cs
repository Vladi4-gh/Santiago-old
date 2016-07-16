using System.Collections.Generic;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Repositories
{
    public interface IPhotographCategoryRepository
    {
        PhotographCategory GetPhotographCategoryById(int id);

        PhotographCategory GetPhotographCategoryByAlias(string alias);

        IEnumerable<PhotographCategory> GetAllPhotographCategoriesOrderedByOrderAsc();

        IEnumerable<PhotographCategory> GetPhotographCategoriesOrderedByCreationDateDesc(int skipNumber, int takeNumber);

        int GetPhotographCategoriesTotalCount();

        int CreatePhotographCategory(PhotographCategory photographCategory);

        void UpdatePhotographCategory(PhotographCategory photographCategory);

        void DeletePhotographCategory(int id);
    }
}