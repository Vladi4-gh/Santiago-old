using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Santiago.Core.Interfaces.Services;
using Santiago.Web.Controllers.BaseControllers;
using Santiago.Web.Helpers;

namespace Santiago.Web.Controllers
{
    [Authorize]
    public class FileUploadController : BaseController
    {
        private readonly IFileService _fileService;

        public FileUploadController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public ActionResult UploadImageFile()
        {
            if (Request.Files.Count > 0)
            {
                var postedFile = Request.Files[0];

                if (postedFile != null && postedFile.ContentLength != 0 && IsImageFile(postedFile))
                {
                    var savedImageFile = _fileService.SaveImageFile(postedFile.FileName, postedFile.InputStream);

                    return Json(SerializeObjectToJson(savedImageFile.ToUploadedImageFileViewModel()));
                }
            }

            return null;
        }

        private static bool IsImageFile(HttpPostedFileBase file)
        {
            var contentTypes = new[] { "image/jpeg", "image/png" };
            var formats = new[] { ".jpe", ".jpeg", ".jpg", ".png" };

            return contentTypes.Any(contentType => file.ContentType.Equals(contentType, StringComparison.OrdinalIgnoreCase)) && formats.Any(format => file.FileName.EndsWith(format, StringComparison.OrdinalIgnoreCase));
        }
    }
}