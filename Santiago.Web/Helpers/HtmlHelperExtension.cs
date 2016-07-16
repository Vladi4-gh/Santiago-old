using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace Santiago.Web.Helpers
{
    public static class HtmlHelperExtension
    {
        public static string GetRequiredCssClassIfAttributeExists(ModelMetadata modelMetadata)
        {
            return modelMetadata.ContainerType.GetProperty(modelMetadata.PropertyName).GetCustomAttributes(typeof(RequiredAttribute), false).Any() ? "required" : string.Empty;
        }

        public static string GetPropertyNameConvertedToLowerCamelCase(string propertyName)
        {
            return char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
        }

        public static MvcHtmlString AdminMenuItemLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string iconClass = null)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var tagBuilder = new TagBuilder("a");

            tagBuilder.Attributes["href"] = urlHelper.Action(actionName, controllerName, new
            {
                area = "Admin"
            });

            tagBuilder.Attributes["title"] = linkText;

            if (string.Equals(actionName, currentAction, StringComparison.CurrentCultureIgnoreCase) && string.Equals(controllerName, currentController, StringComparison.CurrentCultureIgnoreCase))
            {
                tagBuilder.AddCssClass("active");
            }

            if (string.IsNullOrWhiteSpace(iconClass))
            {
                tagBuilder.InnerHtml = linkText;
            }
            else
            {
                tagBuilder.InnerHtml = "<div class='icon-wrapper'><i class='" + iconClass + "'></i></div><div class='link-text-wrapper'>" + linkText + "</div>";
            }

            return MvcHtmlString.Create(tagBuilder.ToString());
        }

        public static MvcHtmlString MainMenuItemLink(this HtmlHelper htmlHelper, string linkText, string url)
        {
            var tagBuilder = new TagBuilder("a");

            tagBuilder.Attributes["href"] = url;
            tagBuilder.Attributes["title"] = linkText;
            tagBuilder.InnerHtml = linkText;

            if (string.Equals(url, htmlHelper.ViewContext.RequestContext.HttpContext.Request.Path, StringComparison.CurrentCultureIgnoreCase))
            {
                tagBuilder.AddCssClass("active");
            }

            return MvcHtmlString.Create(tagBuilder.ToString());
        }

        public static MvcHtmlString DisplayValueFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return DisplayValueFor(helper, expression, null, htmlAttributes);
        }

        public static MvcHtmlString DisplayValueFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string value, object htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("span");

            tagBuilder.MergeAttributes(AnonymousObjectToHtmlAttributes(htmlAttributes));
            tagBuilder.AddCssClass("display-value");
            tagBuilder.Attributes["data-for-field"] = helper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));

            if (!string.IsNullOrEmpty(value))
            {
                tagBuilder.InnerHtml = value;
            }

            return MvcHtmlString.Create(tagBuilder.ToString());
        }

        public static RouteValueDictionary AnonymousObjectToHtmlAttributes(object htmlAttributes)
        {
            var dictionary = new RouteValueDictionary();

            if (htmlAttributes != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(htmlAttributes))
                {
                    dictionary.Add(descriptor.Name.Replace('_', '-'), descriptor.GetValue(htmlAttributes));
                }
            }

            return dictionary;
        }
    }
}