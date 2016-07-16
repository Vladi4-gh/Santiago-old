using System;
using System.ComponentModel.DataAnnotations;

namespace Santiago.Web.Areas.Admin.ViewModels.Photograph
{
    public class EditPhotographViewModel
    {
        [Display(Name = "ID фотографии")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо загрузить фотографию для галереи")]
        [Display(Name = "Фотография для галереи")]
        public int GalleryItemImageFileId { get; set; }

        [Required(ErrorMessage = "Необходимо загрузить фотографию для слайдера галереи")]
        [Display(Name = "Фотография для слайдера галереи")]
        public int GallerySliderImageFileId { get; set; }

        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }

        [MaxLength(256, ErrorMessage = "Длина заголовка фотографии не должна превышать {1} символов")]
        [RegularExpression(@"[^<>]+", ErrorMessage = "Введены недопустимые символы")]
        [Required(ErrorMessage = "Введите заголовок")]
        [Display(Name = "Заголовок", Prompt = "Заголовок фотографии")]
        public string Title { get; set; }

        [MaxLength(256, ErrorMessage = "Длина описания фотографии не должна превышать {1} символов")]
        [RegularExpression(@"[^<>]+", ErrorMessage = "Введены недопустимые символы")]
        [Display(Name = "Описание", Prompt = "Описание фотографии")]
        public string Description { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Дата последнего изменения")]
        public DateTime LastModifiedDate { get; set; }
    }
}