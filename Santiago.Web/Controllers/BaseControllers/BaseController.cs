using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Santiago.Web.Controllers.BaseControllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            // TODO: Вынести в настройку сайта.
            ViewBag.SiteName = "Фотограф Саша Саньтьяго";
        }

        /// <summary>
        /// Сериализует объект в JSON-строку. Приводит имена свойств в lowerCamelCase.
        /// </summary>
        /// <param name="value">Объект для сериализации.</param>
        /// <returns>JSON-строка, представляющая сериализованный объект.</returns>
        protected string SerializeObjectToJson(object value)
        {
            return JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}