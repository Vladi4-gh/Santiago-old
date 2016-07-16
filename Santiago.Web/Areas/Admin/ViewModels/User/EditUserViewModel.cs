using System;
using System.ComponentModel.DataAnnotations;
using Santiago.Web.Attributes.ValidationAttributes.User;

namespace Santiago.Web.Areas.Admin.ViewModels.User
{
    public class EditUserViewModel
    {
        [Display(Name = "ID пользователя")]
        public string Id { get; set; }

        [MaxLength(256, ErrorMessage = "Длина имени не должна превышать {1} символов")]
        [RegularExpression(@"^[a-zA-Z0-9]+", ErrorMessage = "Введены недопустимые символы")]
        [Required(ErrorMessage = "Введите имя")]
        [UniqueUserName]
        [Display(Name = "Имя", Prompt = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Дата последнего изменения")]
        public DateTime LastModifiedDate { get; set; }
    }
}