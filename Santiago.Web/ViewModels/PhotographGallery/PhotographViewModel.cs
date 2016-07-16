namespace Santiago.Web.ViewModels.PhotographGallery
{
    public class PhotographViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public PhotographImageFileViewModel GalleryItemImageFile { get; set; }

        public PhotographImageFileViewModel GallerySliderImageFile { get; set; }
    }
}