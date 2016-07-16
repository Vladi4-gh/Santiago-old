using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Santiago.Web.Areas.Admin.ViewModels.Common;
using Santiago.Web.Areas.Admin.ViewModels.User;
using Santiago.Web.Attributes.ActionFilterAttributes;
using Santiago.Web.Authorization;
using Santiago.Web.Controllers.BaseControllers;
using Santiago.Web.Helpers;

namespace Santiago.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class UserController : UserBaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Пользователи";
            ViewData["CurrentUserId"] = User.Identity.GetUserId();

            return View();
        }

        [HttpGet]
        public JsonResult GetUsersOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            var usersTotalCount = UserManager.Users.Count();
            var model = new DataTableViewModel<int, UserDataTableItemViewModel>
            {
                ItemsTotalCount = usersTotalCount,
                Items = UserManager.Users
                    .OrderByDescending(x => x.CreationDate)
                    .Skip(DataTableHelper.GetRightSkipNumber(skipNumber, takeNumber, usersTotalCount))
                    .Take(takeNumber)
                    .ToList()
                    .ToViewModelList(x => x.ToUserDataTableItemViewModel())
            };

            return Json(SerializeObjectToJson(model), JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void CreateUser(AddUserViewModel model)
        {
            UserManager.Create(new ApplicationUser
            {
                UserName = model.UserName,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            }, model.Password);
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void UpdateUser(EditUserViewModel model)
        {
            var user = UserManager.FindById(model.Id);

            user.UserName = model.UserName;
            user.LastModifiedDate = DateTime.Now;

            UserManager.Update(user);
        }

        [ValidateAntiForgeryToken]
        [ValidateViewModel]
        [HttpPost]
        public void ChangeUserPassword(ChangeUserPasswordViewModel model)
        {
            var user = UserManager.FindById(model.Id);

            user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.NewPassword);
            user.LastModifiedDate = DateTime.Now;

            UserManager.Update(user);
        }

        [HttpPost]
        public void DeleteUser(string id)
        {
            var currentUserId = User.Identity.GetUserId();

            if (currentUserId != id)
            {
                UserManager.Delete(UserManager.FindById(id));
            }
        }
    }
}