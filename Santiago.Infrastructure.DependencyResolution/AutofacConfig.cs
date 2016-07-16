using Autofac;
using Santiago.Core.Interfaces.Logging;
using Santiago.Core.Interfaces.Repositories;
using Santiago.Core.Interfaces.Services;
using Santiago.Infrastructure.Logging;
using Santiago.Infrastructure.Repositories;
using Santiago.Infrastructure.Services;

namespace Santiago.Infrastructure.DependencyResolution
{
    public static class AutofacConfig
    {
        public static IContainer Container { get; private set; }

        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Logger>().As<ILogger>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<ImageFileRepository>().As<IImageFileRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<MainMenuItemRepository>().As<IMainMenuItemRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PageRepository>().As<IPageRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PageTemplateRepository>().As<IPageTemplateRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PhotographCategoryRepository>().As<IPhotographCategoryRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PhotographRepository>().As<IPhotographRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TestimonialRepository>().As<ITestimonialRepository>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<FileService>().As<IFileService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<MainMenuItemService>().As<IMainMenuItemService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PageService>().As<IPageService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PageTemplateService>().As<IPageTemplateService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PhotographCategoryService>().As<IPhotographCategoryService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PhotographService>().As<IPhotographService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TestimonialService>().As<ITestimonialService>().InstancePerLifetimeScope();

            Container = containerBuilder.Build();
        }
    }
}