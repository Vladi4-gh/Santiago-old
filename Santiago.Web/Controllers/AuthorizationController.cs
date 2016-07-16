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
    public class AuthorizationController : UserBaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            return View(new LogInViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> LogIn(LogInViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (result == SignInStatus.Success)
            {
                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            ModelState.AddModelError("InvalidLogInAttempt", "Введены неправильные имя пользователя или пароль");

            return View(model);
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Page", new { area = "Admin" });
            }

            return returnUrl;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("LogIn", "Authorization");
        }
    }
}