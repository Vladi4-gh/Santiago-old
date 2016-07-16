using System.Web.Mvc;

namespace Santiago.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminMainMenu",
                "Admin/Main-Menu/{action}",
                new
                {
                    controller = "MainMenuItem",
                    action = "Index"
                }
            );

            context.MapRoute(
                "AdminPages",
                "Admin/Pages/{action}",
                new
                {
                    controller = "Page",
                    action = "Index"
                }
            );

            context.MapRoute(
                "AdminPhotographCategories",
                "Admin/Photograph-Categories/{action}",
                new
                {
                    controller = "PhotographCategory",
                    action = "Index"
                }
            );

            context.MapRoute(
                "AdminPhotographs",
                "Admin/Photographs/{action}",
                new
                {
                    controller = "Photograph",
                    action = "Index"
                }
            );

            context.MapRoute(
                "AdminSiteSettings",
                "Admin/Site-Settings/{action}",
                new
                {
                    controller = "SiteSetting",
                    action = "Index"
                }
            );

            context.MapRoute(
                "AdminTestimonials",
                "Admin/Testimonials/{action}",
                new
                {
                    controller = "Testimonial",
                    action = "Index"
                }
            );

            context.MapRoute(
                "AdminUsers",
                "Admin/Users/{action}",
                new
                {
                    controller = "User",
                    action = "Index"
                }
            );

            context.MapRoute(
                "Admin",
                "Admin/{action}",
                new
                {
                    controller = "Photograph",
                    action = "Index"
                }
            );
        }
    }
}