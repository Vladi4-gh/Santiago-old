using System.Data.Entity;
using System.Linq;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Repositories;
using Santiago.Infrastructure.Repositories.DataProviding;
using Santiago.Infrastructure.Repositories.DataProviding.DbEntities;
using Santiago.Infrastructure.Repositories.Helpers;

namespace Santiago.Infrastructure.Repositories
{
    public class ImageFileRepository : IImageFileRepository
    {
        public ImageFile GetImageFileById(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbImageFile>()
                    .SingleOrDefault(x => x.Id == id)
                    .ToImageFile();
            }
        }

        public int CreateImageFile(ImageFile imageFile)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbImageFile = new DbImageFile
                {
                    Name = imageFile.Name,
                    Length = imageFile.Length,
                    Width = imageFile.Width,
                    Height = imageFile.Height,
                    UploadDate = imageFile.UploadDate
                };

                context
                    .Set<DbImageFile>()
                    .Add(dbImageFile);

                context.SaveChanges();

                return dbImageFile.Id;
            }
        }

        public void DeleteImageFile(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbImageFile = new DbImageFile
                {
                    Id = id
                };

                context.Entry(dbImageFile).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }
    }
}