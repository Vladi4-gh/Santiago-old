using System;
using System.ComponentModel.DataAnnotations;

namespace Santiago.Web.Areas.Admin.ViewModels.MainMenuItem
{
    public class EditMainMenuItemViewModel
    {
        [Display(Name = "ID пункта")]
        public int Id { get; set; }

        [MaxLength(256, ErrorMessage = "Длина текста не должна превышать {1} символов")]
        [RegularExpression(@"[^<>]+", ErrorMessage = "Введены недопустимые символы")]
        [Required(ErrorMessage = "Введите текст")]
        [Display(Name = "Текст", Prompt = "Текст пункта")]
        public string Text { get; set; }

        [MaxLength(256, ErrorMessage = "Длина ссылки не должна превышать {1} символов")]
        [RegularExpression(@"[^<>]+", ErrorMessage = "Введены недопустимые символы")]
        [Required(ErrorMessage = "Введите ссылку")]
        [Display(Name = "Ссылка", Prompt = "Ссылка в пункте")]
        public string Url { get; set; }

        [Range(0, 2147483647, ErrorMessage = "Число должно быть в пределах от {1} до {2}")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Можно ввести только целое положительное число")]
        [Required(ErrorMessage = "Введите порядок")]
        [Display(Name = "Порядок", Prompt = "Порядок отображения")]
        public int Order { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Дата последнего изменения")]
        public DateTime LastModifiedDate { get; set; }
    }
}