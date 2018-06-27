using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Santiago.Web.Controllers.BaseControllers;
using Santiago.Web.ViewModels.Authorization;

namespace Santiago.Web.Controllers
{
    [Authorize]
    public class SantiagoWebAuthorizationController : UserBaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult SantiagoWebAuthorizationControllerLogIn(string santiagoWebAuthorizationControllerReturnUrl)
        {
            return View(new LogInViewModel
            {
                ReturnUrl = santiagoWebAuthorizationControllerReturnUrl
            });
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> SantiagoWebAuthorizationControllerLogIn(LogInViewModel santiagoWebAuthorizationControllerModel, string santiagoWebAuthorizationControllerReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(santiagoWebAuthorizationControllerModel);
            }

            var result = await SignInManager.PasswordSignInAsync(santiagoWebAuthorizationControllerModel.UserName, santiagoWebAuthorizationControllerModel.Password, santiagoWebAuthorizationControllerModel.RememberMe, false);

            if (result == SignInStatus.Success)
            {
                return Redirect(GetRedirectUrl(santiagoWebAuthorizationControllerModel.ReturnUrl));
            }

            ModelState.AddModelError("InvalidLogInAttempt", "Введены неправильные имя пользователя или пароль");

            return View(santiagoWebAuthorizationControllerModel);
        }

        private string GetRedirectUrl(string santiagoWebAuthorizationControllerReturnUrl)
        {
            if (string.IsNullOrEmpty(santiagoWebAuthorizationControllerReturnUrl) || !Url.IsLocalUrl(santiagoWebAuthorizationControllerReturnUrl))
            {
                return Url.Action("Index", "Page", new { area = "Admin" });
            }

            return santiagoWebAuthorizationControllerReturnUrl;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SantiagoWebAuthorizationControllerLogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("LogIn", "Authorization");
        }
    }
}