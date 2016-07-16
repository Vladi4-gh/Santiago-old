namespace Santiago.Infrastructure.Services.Helpers
{
    /// <summary>
    /// Предоставляет доступ к свойствам, представляющим пути к папке upload и её подпапкам.
    /// </summary>
    public static class UploadFolderPathsHelper
    {
        /// <summary>
        /// Путь к папке upload.
        /// </summary>
        public static string UploadFolderPath
        {
            get { return "/upload/"; }
        }

        /// <summary>
        /// Путь к папке images, в которой хранятся изображения.
        /// </summary>
        public static string UploadImagesFolderPath
        {
            get { return UploadFolderPath + "images/"; }
        }
    }
}