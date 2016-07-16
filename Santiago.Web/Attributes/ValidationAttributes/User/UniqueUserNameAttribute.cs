using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Santiago.Web.Authorization;

namespace Santiago.Web.Attributes.ValidationAttributes.User
{
    public class UniqueUserNameAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Пользователь с таким именем уже существует";

        public string UserNameIdPropertyName { get; set; }

        public UniqueUserNameAttribute(string userNameIdPropertyName = "Id")
        {
            UserNameIdPropertyName = userNameIdPropertyName;
        }

        public string FormatErrorMessage()
        {
            return !string.IsNullOrEmpty(ErrorMessage) ? ErrorMessage : DefaultErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isUserNameUnique;
            var user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByName((string)value);

            if (user == null)
            {
                isUserNameUnique = true;
            }
            else
            {
                var userIdProperty = validationContext.ObjectType.GetProperty(UserNameIdPropertyName);

                if (userIdProperty == null)
                {
                    isUserNameUnique = false;
                }
                else
                {
                    isUserNameUnique = user.Id == (string)userIdProperty.GetValue(validationContext.ObjectInstance, null);
                }
            }

            return isUserNameUnique ? ValidationResult.Success : new ValidationResult(FormatErrorMessage());
        }
    }
}