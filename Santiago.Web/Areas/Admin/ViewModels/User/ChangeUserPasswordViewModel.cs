using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Santiago.Web.Attributes.ValidationAttributes.User;

namespace Santiago.Web.Areas.Admin.ViewModels.User
{
    public class ChangeUserPasswordViewModel
    {
        public string Id { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Введите старый пароль")]
        [UserOldPasswordsMatch]
        [Display(Name = "Старый пароль", Prompt = "Старый пароль")]
        public string OldPassword { get; set; }

        [AllowHtml]
        [MaxLength(256, ErrorMessage = "Длина пароля не должна превышать {1} символов")]
        [MinLength(6, ErrorMessage = "Длина пароля должна быть не менее {1} символов")]
        [Required(ErrorMessage = "Введите новый пароль")]
        [Display(Name = "Новый пароль", Prompt = "Новый пароль")]
        public string NewPassword { get; set; }
    }
}