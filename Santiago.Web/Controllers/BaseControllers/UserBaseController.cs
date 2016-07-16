using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Santiago.Web.Authorization;

namespace Santiago.Web.Controllers.BaseControllers
{
    public abstract class UserBaseController : BaseController
    {
        private SignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public SignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>(); }
            private set { _signInManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        protected UserBaseController()
        {
        }

        protected UserBaseController(ApplicationUserManager userManager, SignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}