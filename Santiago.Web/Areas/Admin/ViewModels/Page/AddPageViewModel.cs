using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Santiago.Web.Areas.Admin.ViewModels.Page
{
    public class AddPageViewModel
    {
        [MaxLength(256, ErrorMessage = "Длина заголовка не должна превышать {1} символов")]
        [RegularExpression(@"[^<>]+", ErrorMessage = "Введены недопустимые символы")]
        [Required(ErrorMessage = "Введите заголовок")]
        [Display(Name = "Заголовок", Prompt = "Заголовок страницы")]
        public string Title { get; set; }

        [MaxLength(256, ErrorMessage = "Длина псевдонима не должна превышать {1} символов")]
        [RegularExpression(@"[\wА-Яа-я-]+", ErrorMessage = "Введены недопустимые символы")]
        [Required(ErrorMessage = "Введите псевдоним")]
        [Display(Name = "Псевдоним", Prompt = "часть-ссылки-на-страницу")]
        public string Alias { get; set; }

        [AllowHtml]
        [Display(Name = "Текст", Prompt = "Текст или <p>HTML-вёрстка</p>")]
        public string Text { get; set; }

        [MaxLength(256, ErrorMessage = "Описание страницы не должно превышать {1} символов")]
        [RegularExpression(@"[^<>]+", ErrorMessage = "Введены недопустимые символы")]
        [Display(Name = "Описание страницы", Prompt = "Краткое описание страницы")]
        public string MetaDescription { get; set; }

        [MaxLength(256, ErrorMessage = "Общая длина ключевых слов не должна превышать {1} символов")]
        [RegularExpression(@"[^<>]+", ErrorMessage = "Введены недопустимые символы")]
        [Display(Name = "Ключевые слова", Prompt = "Ключевые слова через запятую")]
        public string MetaKeywords { get; set; }

        [Display(Name = "Родительская страница")]
        public int? ParentId { get; set; }

        [Display(Name = "Частота изменения (для sitemap.xml)")]
        public string SitemapXmlChangeFrequency { get; set; }

        [Display(Name = "Приоритет (для sitemap.xml)")]
        public float? SitemapXmlPriority { get; set; }

        [Required(ErrorMessage = "Выберите шаблон страницы")]
        [Display(Name = "Шаблон страницы")]
        public int TemplateId { get; set; }

        [Display(Name = "Статус публикации")]
        public bool IsPublished { get; set; }
    }
}