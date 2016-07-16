using System;
using Santiago.Web.Areas.Admin.ViewModels.ImageFile;

namespace Santiago.Web.Areas.Admin.ViewModels.Photograph
{
    public class PhotographDataTableItemViewModel
    {
        public int Id { get; set; }

        public int GalleryItemImageFileId { get; set; }

        public ImageFileDataTableItemViewModel GalleryItemImageFile { get; set; }

        public int GallerySliderImageFileId { get; set; }

        public ImageFileDataTableItemViewModel GallerySliderImageFile { get; set; }

        public int? CategoryId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}