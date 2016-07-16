using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Santiago.Web.ViewModels.Authorization
{
    public class LogInViewModel
    {
        public string ReturnUrl { get; set; }

        [RegularExpression(@"[^<>]+", ErrorMessage = "Введены недопустимые символы")]
        [Required(ErrorMessage = "Введите имя пользователя")]
        [Display(Name = "Имя пользователя", Prompt = "Имя пользователя")]
        public string UserName { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль", Prompt = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}