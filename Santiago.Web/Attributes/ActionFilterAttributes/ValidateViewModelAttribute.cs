using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Santiago.Web.Attributes.ActionFilterAttributes
{
    public class ValidateViewModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;

            if (!modelState.IsValid)
            {
                filterContext.Result = new JsonResult
                {
                    // При возврате объекта ошибок он сериализуется в JSON-строку, но имена свойств не преобразуются в lowerCamelCase,
                    // т.к. для отображения ошибок серверной валидации используется метод showErrors плагина jQuery Validation,
                    // который ищет input'ы, у которых нужно отобразить ошибку, по их атрибуту name,
                    // а адекватного способа указать HtmlHelper'ам ASP.NET MVC всегда приводить имена свойств в lowerCamelCase нет.
                    Data = JsonConvert.SerializeObject(modelState
                        .Where(x => x.Value.Errors.Any())
                        .ToDictionary(
                            x => x.Key,
                            x => string.Join("; ", x.Value.Errors.Select(y => y.ErrorMessage))))
                };
            }

            base.OnActionExecuting(filterContext);
        }
    }
}