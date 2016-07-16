using Santiago.Core.Entities;

namespace Santiago.Core.Interfaces.Repositories
{
    public interface IImageFileRepository
    {
        ImageFile GetImageFileById(int id);

        int CreateImageFile(ImageFile imageFile);

        void DeleteImageFile(int id);
    }
}