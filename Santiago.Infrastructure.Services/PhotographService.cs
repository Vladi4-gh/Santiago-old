using System.Collections.Generic;
using System.Linq;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Repositories;
using Santiago.Core.Interfaces.Services;
using Santiago.Infrastructure.Services.Helpers;

namespace Santiago.Infrastructure.Services
{
    public class PhotographService : IPhotographService
    {
        private readonly IPhotographRepository _photographRepository;

        private readonly IFileService _fileService;

        public PhotographService(IPhotographRepository photographRepository, IFileService fileService)
        {
            _photographRepository = photographRepository;
            _fileService = fileService;
        }

        public Photograph GetPhotographById(int id)
        {
            return _photographRepository.GetPhotographById(id);
        }

        public List<Photograph> GetPhotographsOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            var photographs = _photographRepository.GetPhotographsOrderedByCreationDateDesc(skipNumber, takeNumber).ToList();

            foreach (var photograph in photographs)
            {
                photograph.GalleryItemImageFile.Url = UploadFolderPathsHelper.UploadImagesFolderPath + photograph.GalleryItemImageFile.Name;
                photograph.GallerySliderImageFile.Url = UploadFolderPathsHelper.UploadImagesFolderPath + photograph.GallerySliderImageFile.Name;
            }

            return photographs;
        }

        public List<Photograph> GetPhotographsByCategoryAliasOrderedByCreationDateDesc(string categoryAlias, int skipNumber, int takeNumber)
        {
            var photographs = _photographRepository.GetPhotographsByCategoryAliasOrderedByCreationDateDesc(categoryAlias, skipNumber, takeNumber).ToList();

            foreach (var photograph in photographs)
            {
                photograph.GalleryItemImageFile.Url = UploadFolderPathsHelper.UploadImagesFolderPath + photograph.GalleryItemImageFile.Name;
                photograph.GallerySliderImageFile.Url = UploadFolderPathsHelper.UploadImagesFolderPath + photograph.GallerySliderImageFile.Name;
            }

            return photographs;
        }

        public int GetPhotographsTotalCount()
        {
            return _photographRepository.GetPhotographsTotalCount();
        }

        public int CreatePhotograph(Photograph photograph)
        {
            return _photographRepository.CreatePhotograph(photograph);
        }

        public void UpdatePhotograph(Photograph photograph)
        {
            _photographRepository.UpdatePhotograph(photograph);
        }

        public void DeletePhotograph(int id)
        {
            var selectedPhotograph = _photographRepository.GetPhotographById(id);

            if (selectedPhotograph != null)
            {
                _photographRepository.DeletePhotograph(selectedPhotograph.Id);

                _fileService.DeleteImageFile(selectedPhotograph.GalleryItemImageFileId);
                _fileService.DeleteImageFile(selectedPhotograph.GallerySliderImageFileId);
            }
        }
    }
}