using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Repositories;
using Santiago.Core.Interfaces.Services;
using Santiago.Infrastructure.Services.Helpers;

namespace Santiago.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IImageFileRepository _imageFileRepository;

        public FileService(IImageFileRepository imageFileRepository)
        {
            _imageFileRepository = imageFileRepository;
        }

        public ImageFile SaveImageFile(string fileName, Stream fileStream)
        {
            var savedImageFileName = SaveFile(UploadFolderPathsHelper.UploadImagesFolderPath, fileName, fileStream);
            var image = Image.FromStream(fileStream);
            var imageFile = new ImageFile
            {
                Name = savedImageFileName,
                Url = UploadFolderPathsHelper.UploadImagesFolderPath + savedImageFileName,
                Length = fileStream.Length,
                Width = image.Width,
                Height = image.Height,
                UploadDate = DateTime.Now
            };

            var createdImageFileId = _imageFileRepository.CreateImageFile(imageFile);

            imageFile.Id = createdImageFileId;

            return imageFile;
        }

        public void DeleteImageFile(int id)
        {
            var imageFile = _imageFileRepository.GetImageFileById(id);

            if (imageFile != null)
            {
                _imageFileRepository.DeleteImageFile(id);

                DeleteFile(UploadFolderPathsHelper.UploadImagesFolderPath, imageFile.Name);
            }
        }

        private string SaveFile(string uploadFolderPath, string fileName, Stream fileStream)
        {
            var fullUploadFolderPath = GetFullUploadFolderPath(uploadFolderPath);

            Directory.CreateDirectory(fullUploadFolderPath);

            if (File.Exists(Path.Combine(fullUploadFolderPath, fileName)))
            {
                fileName = GetUniqueFileName(fullUploadFolderPath, fileName);
            }

            using (var createdFileStream = File.Create(Path.Combine(fullUploadFolderPath, fileName)))
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.CopyTo(createdFileStream);
            }

            return fileName;
        }

        private void DeleteFile(string uploadFolderPath, string fileName)
        {
            var fullUploadFolderPath = GetFullUploadFolderPath(uploadFolderPath);

            File.Delete(Path.Combine(fullUploadFolderPath, fileName));
        }

        private string GetFullUploadFolderPath(string uploadFolderPath)
        {
            var fullUploadFolderPathSegments = new List<string>
            {
                AppDomain.CurrentDomain.BaseDirectory
            };

            fullUploadFolderPathSegments.AddRange(uploadFolderPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries));

            return Path.Combine(fullUploadFolderPathSegments.ToArray());
        }

        private string GetUniqueFileName(string fullUploadFolderPath, string fileName)
        {
            var lastDotIndex = fileName.LastIndexOf('.');
            var isFileNameUnique = false;
            var i = 1;

            do
            {
                var temporaryFileName = lastDotIndex > -1 ? fileName.Insert(lastDotIndex, "_" + i) : fileName + "_" + i;

                if (File.Exists(Path.Combine(fullUploadFolderPath, temporaryFileName)))
                {
                    i++;
                }
                else
                {
                    fileName = temporaryFileName;
                    isFileNameUnique = true;
                }
            } while (!isFileNameUnique);

            return fileName;
        }
    }
}