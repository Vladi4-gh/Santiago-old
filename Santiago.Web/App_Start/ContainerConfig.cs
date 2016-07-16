using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Santiago.Infrastructure.DependencyResolution;

namespace Santiago.Web
{
    public static class ContainerConfig
    {
        public static void Register()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterControllers(Assembly.GetAssembly(typeof(MvcApplication)));
            containerBuilder.RegisterModule(new AutofacWebTypesModule());

            AutofacConfig.Register(containerBuilder);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacConfig.Container));
        }
    }
}