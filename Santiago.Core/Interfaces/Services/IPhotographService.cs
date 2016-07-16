using System.Collections.Generic;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Services
{
    public interface IPhotographService
    {
        Photograph GetPhotographById(int id);

        List<Photograph> GetPhotographsOrderedByCreationDateDesc(int skipNumber, int takeNumber);

        List<Photograph> GetPhotographsByCategoryAliasOrderedByCreationDateDesc(string categoryAlias, int skipNumber, int takeNumber);

        int GetPhotographsTotalCount();

        int CreatePhotograph(Photograph photograph);

        void UpdatePhotograph(Photograph photograph);

        void DeletePhotograph(int id);
    }
}