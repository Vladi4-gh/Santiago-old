using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Santiago.Web.Attributes.ValidationAttributes.User;

namespace Santiago.Web.Areas.Admin.ViewModels.User
{
    public class AddUserViewModel
    {
        [MaxLength(256, ErrorMessage = "Длина имени не должна превышать {1} символов")]
        [RegularExpression(@"^[a-zA-Z0-9]+", ErrorMessage = "Введены недопустимые символы")]
        [Required(ErrorMessage = "Введите имя")]
        [UniqueUserName]
        [Display(Name = "Имя", Prompt = "Имя пользователя")]
        public string UserName { get; set; }

        [AllowHtml]
        [MaxLength(256, ErrorMessage = "Длина пароля не должна превышать {1} символов")]
        [MinLength(6, ErrorMessage = "Длина пароля должна быть не менее {1} символов")]
        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль", Prompt = "Пароль")]
        public string Password { get; set; }
    }
}