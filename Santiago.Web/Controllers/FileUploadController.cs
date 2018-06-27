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
    public class SantiagoWebFileUploadController : BaseController
    {
        private readonly IFileService _santiagoFileUpladControllerFileService;

        public SantiagoWebFileUploadController(IFileService santiagoFileUpladControllerFileService)
        {
            _santiagoFileUpladControllerFileService = santiagoFileUpladControllerFileService;
        }

        [HttpPost]
        public ActionResult UploadImageFile()
        {
            if (Request.Files.Count > 0)
            {
                var santiagoWebUploadedImagePostedFile = Request.Files[0];

                if (santiagoWebUploadedImagePostedFile != null && santiagoWebUploadedImagePostedFile.ContentLength != 0 && IsImageFile(santiagoWebUploadedImagePostedFile))
                {
                    var santiagoWebUploadedImageSavedImageFile = _santiagoFileUpladControllerFileService.SaveImageFile(santiagoWebUploadedImagePostedFile.FileName, santiagoWebUploadedImagePostedFile.InputStream);

                    return Json(SerializeObjectToJson(santiagoWebUploadedImageSavedImageFile.ToUploadedImageFileViewModel()));
                }
            }

            return null;
        }

        private static bool IsImageFile(HttpPostedFileBase santiagoFileUpladControllerPostedFile)
        {
            var santiagoFileUpladControllerContentTypes = new[] { "image/jpeg", "image/png" };
            var santiagoFileUpladControllerFormats = new[] { ".jpe", ".jpeg", ".jpg", ".png" };

            return santiagoFileUpladControllerContentTypes.Any(santiagoFileUpladControllerContentType => santiagoFileUpladControllerPostedFile.ContentType.Equals(santiagoFileUpladControllerContentType, StringComparison.OrdinalIgnoreCase)) && santiagoFileUpladControllerFormats.Any(santiagoFileUpladControllerFormat => santiagoFileUpladControllerPostedFile.FileName.EndsWith(santiagoFileUpladControllerFormat, StringComparison.OrdinalIgnoreCase));
        }
    }
}