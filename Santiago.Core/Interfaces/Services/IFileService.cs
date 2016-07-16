using System.IO;
using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Services
{
    public interface IFileService
    {
        ImageFile SaveImageFile(string fileName, Stream fileStream);

        void DeleteImageFile(int id);
    }
}