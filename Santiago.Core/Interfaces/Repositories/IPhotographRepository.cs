using System.Collections.Generic;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Repositories
{
    public interface IPhotographRepository
    {
        Photograph GetPhotographById(int id);

        IEnumerable<Photograph> GetPhotographsOrderedByCreationDateDesc(int skipNumber, int takeNumber);

        IEnumerable<Photograph> GetPhotographsByCategoryAliasOrderedByCreationDateDesc(string categoryAlias, int skipNumber, int takeNumber);

        int GetPhotographsTotalCount();

        int CreatePhotograph(Photograph photograph);

        void UpdatePhotograph(Photograph photograph);

        void DeletePhotograph(int id);
    }
}