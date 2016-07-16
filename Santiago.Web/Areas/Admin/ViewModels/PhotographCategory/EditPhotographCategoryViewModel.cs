using System;
using System.ComponentModel.DataAnnotations;
using Santiago.Web.Attributes.ValidationAttributes.PhotographCategory;

namespace Santiago.Web.Areas.Admin.ViewModels.PhotographCategory
{
    public class EditPhotographCategoryViewModel
    {
        [Display(Name = "ID категории")]
        public int Id { get; set; }

        [MaxLength(256, ErrorMessage = "Длина названия не должна превышать {1} символов")]
        [RegularExpression(@"[^<>]+", ErrorMessage = "Введены недопустимые символы")]
        [Required(ErrorMessage = "Введите название")]
        [Display(Name = "Название", Prompt = "Название категории")]
        public string Name { get; set; }

        [MaxLength(256, ErrorMessage = "Длина псевдонима не должна превышать {1} символов")]
        [RegularExpression(@"[\wА-Яа-я-]+", ErrorMessage = "Введены недопустимые символы")]
        [Required(ErrorMessage = "Введите псевдоним")]
        [UniquePhotographCategoryAlias]
        [Display(Name = "Псевдоним", Prompt = "псевдоним-категории")]
        public string Alias { get; set; }

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