using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Santiago.Web.Authorization;

namespace Santiago.Web.Attributes.ValidationAttributes.User
{
    public class UserOldPasswordsMatchAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Старый пароль введён неверно";

        public string UserIdPropertyName { get; set; }

        public UserOldPasswordsMatchAttribute(string userIdPropertyName = "Id")
        {
            UserIdPropertyName = userIdPropertyName;
        }

        public string FormatErrorMessage()
        {
            return !string.IsNullOrEmpty(ErrorMessage) ? ErrorMessage : DefaultErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userIdProperty = validationContext.ObjectType.GetProperty(UserIdPropertyName);
            var userIdPropertyValue = (string)userIdProperty.GetValue(validationContext.ObjectInstance, null);
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userPasswordHash = userManager.FindById(userIdPropertyValue).PasswordHash;
            var oldPasswordsMatchResult = userManager.PasswordHasher.VerifyHashedPassword(userPasswordHash, (string)value);

            return oldPasswordsMatchResult == PasswordVerificationResult.Failed ? new ValidationResult(FormatErrorMessage()) : ValidationResult.Success;
        }
    }
}