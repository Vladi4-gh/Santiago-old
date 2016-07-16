using System.Web.Mvc;
using Santiago.Core.Entities;
using Santiago.Web.Controllers.BaseControllers;

namespace Santiago.Web.Controllers.PageControllers
{
    public class TextController : PageBaseController
    {
        [HttpGet]
        public ActionResult Index(Page page)
        {
            ViewBag.MetaDescription = page.MetaDescription;
            ViewBag.MetaKeywords = page.MetaKeywords;
            ViewBag.PageTitle = page.Title;
            ViewBag.Text = page.Text;

            return View();
        }
    }
}