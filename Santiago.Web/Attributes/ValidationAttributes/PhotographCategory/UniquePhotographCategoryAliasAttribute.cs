using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Autofac;
using Santiago.Core.Interfaces.Services;
using Santiago.Infrastructure.DependencyResolution;

namespace Santiago.Web.Attributes.ValidationAttributes.PhotographCategory
{
    public class UniquePhotographCategoryAliasAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Этот псевдоним уже занят категорией {0} (ID: {1})";

        public string PhotographCategoryIdPropertyName { get; set; }

        public UniquePhotographCategoryAliasAttribute(string photographCategoryIdPropertyName = "Id")
        {
            PhotographCategoryIdPropertyName = photographCategoryIdPropertyName;
        }

        public string FormatErrorMessage(Core.Entities.PhotographCategory photographCategory)
        {
            return string.Format(CultureInfo.InvariantCulture, !string.IsNullOrEmpty(ErrorMessage) ? ErrorMessage : DefaultErrorMessage, photographCategory.Name, photographCategory.Id);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isPhotographCategoryAliasUnique;
            var photographCategory = AutofacConfig.Container.Resolve<IPhotographCategoryService>().GetPhotographCategoryByAlias((string)value);

            if (photographCategory == null)
            {
                isPhotographCategoryAliasUnique = true;
            }
            else
            {
                var photographCategoryIdProperty = validationContext.ObjectType.GetProperty(PhotographCategoryIdPropertyName);

                if (photographCategoryIdProperty == null)
                {
                    isPhotographCategoryAliasUnique = false;
                }
                else
                {
                    isPhotographCategoryAliasUnique = photographCategory.Id == (int)photographCategoryIdProperty.GetValue(validationContext.ObjectInstance, null);
                }
            }

            return isPhotographCategoryAliasUnique ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(photographCategory));
        }
    }
}