using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Santiago.Core.Entities;
using Santiago.Web.Areas.Admin.ViewModels.Common;
using Santiago.Web.Areas.Admin.ViewModels.ImageFile;
using Santiago.Web.Areas.Admin.ViewModels.MainMenuItem;
using Santiago.Web.Areas.Admin.ViewModels.Page;
using Santiago.Web.Areas.Admin.ViewModels.Photograph;
using Santiago.Web.Areas.Admin.ViewModels.PhotographCategory;
using Santiago.Web.Areas.Admin.ViewModels.Testimonial;
using Santiago.Web.Areas.Admin.ViewModels.User;
using Santiago.Web.Authorization;
using Santiago.Web.ViewModels.Common;
using Santiago.Web.ViewModels.FileUpload;
using Santiago.Web.ViewModels.PhotographGallery;
using Santiago.Web.ViewModels.Testimonial;

namespace Santiago.Web.Helpers
{
    internal static class ConvertHelper
    {
        private static readonly IMapper mapper;

        static ConvertHelper()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                // Entity в ViewModel.

                configuration.CreateMap<ApplicationUser, UserDataTableItemViewModel>()
                    .ForMember(viewModel => viewModel.Id, map => map.MapFrom(entity => entity.Id))
                    .ForMember(viewModel => viewModel.UserName, map => map.MapFrom(entity => entity.UserName))
                    .ForMember(viewModel => viewModel.CreationDate, map => map.MapFrom(entity => entity.CreationDate))
                    .ForMember(viewModel => viewModel.LastModifiedDate, map => map.MapFrom(entity => entity.LastModifiedDate));

                configuration.CreateMap<ImageFile, ImageFileDataTableItemViewModel>()
                    .ForMember(viewModel => viewModel.Id, map => map.MapFrom(entity => entity.Id))
                    .ForMember(viewModel => viewModel.Name, map => map.MapFrom(entity => entity.Name))
                    .ForMember(viewModel => viewModel.Url, map => map.MapFrom(entity => entity.Url))
                    .ForMember(viewModel => viewModel.Length, map => map.MapFrom(entity => entity.Length))
                    .ForMember(viewModel => viewModel.Width, map => map.MapFrom(entity => entity.Width))
                    .ForMember(viewModel => viewModel.Height, map => map.MapFrom(entity => entity.Height))
                    .ForMember(viewModel => viewModel.UploadDate, map => map.MapFrom(entity => entity.UploadDate));

                configuration.CreateMap<ImageFile, PhotographImageFileViewModel>()
                    .ForMember(viewModel => viewModel.Url, map => map.MapFrom(entity => Properties.Settings.Default.UseCDNForPhotographGalleryImages ? CombineUrls(Properties.Settings.Default.PhotographGalleryImagesCDNUrl, entity.Url) : entity.Url))
                    .ForMember(viewModel => viewModel.Width, map => map.MapFrom(entity => entity.Width))
                    .ForMember(viewModel => viewModel.Height, map => map.MapFrom(entity => entity.Height));

                configuration.CreateMap<ImageFile, TestimonialAuthorImageFileViewModel>()
                    .ForMember(viewModel => viewModel.Url, map => map.MapFrom(entity => entity.Url))
                    .ForMember(viewModel => viewModel.Width, map => map.MapFrom(entity => entity.Width))
                    .ForMember(viewModel => viewModel.Height, map => map.MapFrom(entity => entity.Height));

                configuration.CreateMap<ImageFile, UploadedImageFileViewModel>()
                    .ForMember(viewModel => viewModel.Id, map => map.MapFrom(entity => entity.Id))
                    .ForMember(viewModel => viewModel.Name, map => map.MapFrom(entity => entity.Name))
                    .ForMember(viewModel => viewModel.Url, map => map.MapFrom(entity => entity.Url))
                    .ForMember(viewModel => viewModel.Length, map => map.MapFrom(entity => entity.Length))
                    .ForMember(viewModel => viewModel.Width, map => map.MapFrom(entity => entity.Width))
                    .ForMember(viewModel => viewModel.Height, map => map.MapFrom(entity => entity.Height))
                    .ForMember(viewModel => viewModel.UploadDate, map => map.MapFrom(entity => entity.UploadDate));

                configuration.CreateMap<MainMenuItem, MainMenuItemViewModel>()
                    .ForMember(viewModel => viewModel.Text, map => map.MapFrom(entity => entity.Text))
                    .ForMember(viewModel => viewModel.Url, map => map.MapFrom(entity => entity.Url));

                configuration.CreateMap<MainMenuItem, MainMenuItemDataTableItemViewModel>()
                    .ForMember(viewModel => viewModel.Id, map => map.MapFrom(entity => entity.Id))
                    .ForMember(viewModel => viewModel.Text, map => map.MapFrom(entity => entity.Text))
                    .ForMember(viewModel => viewModel.Url, map => map.MapFrom(entity => entity.Url))
                    .ForMember(viewModel => viewModel.Order, map => map.MapFrom(entity => entity.Order))
                    .ForMember(viewModel => viewModel.CreationDate, map => map.MapFrom(entity => entity.CreationDate))
                    .ForMember(viewModel => viewModel.LastModifiedDate, map => map.MapFrom(entity => entity.LastModifiedDate));

                configuration.CreateMap<Page, PageDataTableItemViewModel>()
                    .ForMember(viewModel => viewModel.Id, map => map.MapFrom(entity => entity.Id))
                    .ForMember(viewModel => viewModel.Title, map => map.MapFrom(entity => entity.Title))
                    .ForMember(viewModel => viewModel.Alias, map => map.MapFrom(entity => entity.Alias))
                    .ForMember(viewModel => viewModel.Text, map => map.MapFrom(entity => entity.Text))
                    .ForMember(viewModel => viewModel.MetaDescription, map => map.MapFrom(entity => entity.MetaDescription))
                    .ForMember(viewModel => viewModel.MetaKeywords, map => map.MapFrom(entity => entity.MetaKeywords))
                    .ForMember(viewModel => viewModel.ParentId, map => map.MapFrom(entity => entity.ParentId))
                    .ForMember(viewModel => viewModel.ParentTitle, map => map.MapFrom(entity => entity.Parent.Title))
                    .ForMember(viewModel => viewModel.SitemapXmlChangeFrequency, map => map.MapFrom(entity => entity.SitemapXmlChangeFrequency))
                    .ForMember(viewModel => viewModel.SitemapXmlPriority, map => map.MapFrom(entity => entity.SitemapXmlPriority))
                    .ForMember(viewModel => viewModel.TemplateId, map => map.MapFrom(entity => entity.TemplateId))
                    .ForMember(viewModel => viewModel.IsPublished, map => map.MapFrom(entity => entity.IsPublished))
                    .ForMember(viewModel => viewModel.CreationDate, map => map.MapFrom(entity => entity.CreationDate))
                    .ForMember(viewModel => viewModel.LastModifiedDate, map => map.MapFrom(entity => entity.LastModifiedDate))
                    .ForMember(viewModel => viewModel.PublicationDate, map => map.MapFrom(entity => entity.PublicationDate));

                configuration.CreateMap<PageTemplate, SelectListItemViewModel>()
                    .ForMember(viewModel => viewModel.Value, map => map.MapFrom(entity => entity.Id))
                    .ForMember(viewModel => viewModel.Text, map => map.MapFrom(entity => entity.Title));

                configuration.CreateMap<Photograph, PhotographViewModel>()
                    .ForMember(viewModel => viewModel.Title, map => map.MapFrom(entity => entity.Title))
                    .ForMember(viewModel => viewModel.Description, map => map.MapFrom(entity => entity.Description))
                    .ForMember(viewModel => viewModel.GalleryItemImageFile, map => map.MapFrom(entity => entity.GalleryItemImageFile))
                    .ForMember(viewModel => viewModel.GallerySliderImageFile, map => map.MapFrom(entity => entity.GallerySliderImageFile));

                configuration.CreateMap<Photograph, PhotographDataTableItemViewModel>()
                    .ForMember(viewModel => viewModel.Id, map => map.MapFrom(entity => entity.Id))
                    .ForMember(viewModel => viewModel.GalleryItemImageFileId, map => map.MapFrom(entity => entity.GalleryItemImageFileId))
                    .ForMember(viewModel => viewModel.GalleryItemImageFile, map => map.MapFrom(entity => entity.GalleryItemImageFile))
                    .ForMember(viewModel => viewModel.GallerySliderImageFileId, map => map.MapFrom(entity => entity.GallerySliderImageFileId))
                    .ForMember(viewModel => viewModel.GallerySliderImageFile, map => map.MapFrom(entity => entity.GallerySliderImageFile))
                    .ForMember(viewModel => viewModel.CategoryId, map => map.MapFrom(entity => entity.CategoryId))
                    .ForMember(viewModel => viewModel.Title, map => map.MapFrom(entity => entity.Title))
                    .ForMember(viewModel => viewModel.Description, map => map.MapFrom(entity => entity.Description))
                    .ForMember(viewModel => viewModel.CreationDate, map => map.MapFrom(entity => entity.CreationDate))
                    .ForMember(viewModel => viewModel.LastModifiedDate, map => map.MapFrom(entity => entity.LastModifiedDate));

                configuration.CreateMap<PhotographCategory, PhotographCategoryViewModel>()
                    .ForMember(viewModel => viewModel.Name, map => map.MapFrom(entity => entity.Name))
                    .ForMember(viewModel => viewModel.Alias, map => map.MapFrom(entity => entity.Alias));

                configuration.CreateMap<PhotographCategory, PhotographCategoryDataTableItemViewModel>()
                    .ForMember(viewModel => viewModel.Id, map => map.MapFrom(entity => entity.Id))
                    .ForMember(viewModel => viewModel.Name, map => map.MapFrom(entity => entity.Name))
                    .ForMember(viewModel => viewModel.Alias, map => map.MapFrom(entity => entity.Alias))
                    .ForMember(viewModel => viewModel.Order, map => map.MapFrom(entity => entity.Order))
                    .ForMember(viewModel => viewModel.CreationDate, map => map.MapFrom(entity => entity.CreationDate))
                    .ForMember(viewModel => viewModel.LastModifiedDate, map => map.MapFrom(entity => entity.LastModifiedDate));

                configuration.CreateMap<Testimonial, TestimonialViewModel>()
                    .ForMember(viewModel => viewModel.AuthorName, map => map.MapFrom(entity => entity.AuthorName))
                    .ForMember(viewModel => viewModel.AuthorImageFile, map => map.MapFrom(entity => entity.AuthorImageFile))
                    .ForMember(viewModel => viewModel.Text, map => map.MapFrom(entity => entity.Text))
                    .ForMember(viewModel => viewModel.CreationDate, map => map.MapFrom(entity => entity.CreationDate));

                configuration.CreateMap<Testimonial, TestimonialDataTableItemViewModel>()
                    .ForMember(viewModel => viewModel.Id, map => map.MapFrom(entity => entity.Id))
                    .ForMember(viewModel => viewModel.AuthorName, map => map.MapFrom(entity => entity.AuthorName))
                    .ForMember(viewModel => viewModel.AuthorImageFileId, map => map.MapFrom(entity => entity.AuthorImageFileId))
                    .ForMember(viewModel => viewModel.AuthorImageFile, map => map.MapFrom(entity => entity.AuthorImageFile))
                    .ForMember(viewModel => viewModel.Text, map => map.MapFrom(entity => entity.Text))
                    .ForMember(viewModel => viewModel.CreationDate, map => map.MapFrom(entity => entity.CreationDate))
                    .ForMember(viewModel => viewModel.LastModifiedDate, map => map.MapFrom(entity => entity.LastModifiedDate));


                // ViewModel в Entity.

                configuration.CreateMap<AddMainMenuItemViewModel, MainMenuItem>()
                    .ForMember(entity => entity.Text, map => map.MapFrom(viewModel => viewModel.Text))
                    .ForMember(entity => entity.Url, map => map.MapFrom(viewModel => viewModel.Url))
                    .ForMember(entity => entity.Order, map => map.MapFrom(viewModel => viewModel.Order));

                configuration.CreateMap<EditMainMenuItemViewModel, MainMenuItem>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(viewModel => viewModel.Id))
                    .ForMember(entity => entity.Text, map => map.MapFrom(viewModel => viewModel.Text))
                    .ForMember(entity => entity.Url, map => map.MapFrom(viewModel => viewModel.Url))
                    .ForMember(entity => entity.Order, map => map.MapFrom(viewModel => viewModel.Order));

                configuration.CreateMap<AddPageViewModel, Page>()
                    .ForMember(entity => entity.Title, map => map.MapFrom(viewModel => viewModel.Title))
                    .ForMember(entity => entity.Alias, map => map.MapFrom(viewModel => viewModel.Alias))
                    .ForMember(entity => entity.Text, map => map.MapFrom(viewModel => viewModel.Text))
                    .ForMember(entity => entity.MetaDescription, map => map.MapFrom(viewModel => viewModel.MetaDescription))
                    .ForMember(entity => entity.MetaKeywords, map => map.MapFrom(viewModel => viewModel.MetaKeywords))
                    .ForMember(entity => entity.ParentId, map => map.MapFrom(viewModel => viewModel.ParentId))
                    .ForMember(entity => entity.SitemapXmlChangeFrequency, map => map.MapFrom(viewModel => viewModel.SitemapXmlChangeFrequency))
                    .ForMember(entity => entity.SitemapXmlPriority, map => map.MapFrom(viewModel => viewModel.SitemapXmlPriority))
                    .ForMember(entity => entity.TemplateId, map => map.MapFrom(viewModel => viewModel.TemplateId))
                    .ForMember(entity => entity.IsPublished, map => map.MapFrom(viewModel => viewModel.IsPublished));

                configuration.CreateMap<EditPageViewModel, Page>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(viewModel => viewModel.Id))
                    .ForMember(entity => entity.Title, map => map.MapFrom(viewModel => viewModel.Title))
                    .ForMember(entity => entity.Alias, map => map.MapFrom(viewModel => viewModel.Alias))
                    .ForMember(entity => entity.Text, map => map.MapFrom(viewModel => viewModel.Text))
                    .ForMember(entity => entity.MetaDescription, map => map.MapFrom(viewModel => viewModel.MetaDescription))
                    .ForMember(entity => entity.MetaKeywords, map => map.MapFrom(viewModel => viewModel.MetaKeywords))
                    .ForMember(entity => entity.ParentId, map => map.MapFrom(viewModel => viewModel.ParentId))
                    .ForMember(entity => entity.SitemapXmlChangeFrequency, map => map.MapFrom(viewModel => viewModel.SitemapXmlChangeFrequency))
                    .ForMember(entity => entity.SitemapXmlPriority, map => map.MapFrom(viewModel => viewModel.SitemapXmlPriority))
                    .ForMember(entity => entity.TemplateId, map => map.MapFrom(viewModel => viewModel.TemplateId))
                    .ForMember(entity => entity.IsPublished, map => map.MapFrom(viewModel => viewModel.IsPublished));

                configuration.CreateMap<AddPhotographViewModel, Photograph>()
                    .ForMember(entity => entity.GalleryItemImageFileId, map => map.MapFrom(viewModel => viewModel.GalleryItemImageFileId))
                    .ForMember(entity => entity.GallerySliderImageFileId, map => map.MapFrom(viewModel => viewModel.GallerySliderImageFileId))
                    .ForMember(entity => entity.CategoryId, map => map.MapFrom(viewModel => viewModel.CategoryId))
                    .ForMember(entity => entity.Title, map => map.MapFrom(viewModel => viewModel.Title))
                    .ForMember(entity => entity.Description, map => map.MapFrom(viewModel => viewModel.Description));

                configuration.CreateMap<EditPhotographViewModel, Photograph>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(viewModel => viewModel.Id))
                    .ForMember(entity => entity.GalleryItemImageFileId, map => map.MapFrom(viewModel => viewModel.GalleryItemImageFileId))
                    .ForMember(entity => entity.GallerySliderImageFileId, map => map.MapFrom(viewModel => viewModel.GallerySliderImageFileId))
                    .ForMember(entity => entity.CategoryId, map => map.MapFrom(viewModel => viewModel.CategoryId))
                    .ForMember(entity => entity.Title, map => map.MapFrom(viewModel => viewModel.Title))
                    .ForMember(entity => entity.Description, map => map.MapFrom(viewModel => viewModel.Description));

                configuration.CreateMap<AddPhotographCategoryViewModel, PhotographCategory>()
                    .ForMember(entity => entity.Name, map => map.MapFrom(viewModel => viewModel.Name))
                    .ForMember(entity => entity.Alias, map => map.MapFrom(viewModel => viewModel.Alias))
                    .ForMember(entity => entity.Order, map => map.MapFrom(viewModel => viewModel.Order));

                configuration.CreateMap<EditPhotographCategoryViewModel, PhotographCategory>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(viewModel => viewModel.Id))
                    .ForMember(entity => entity.Name, map => map.MapFrom(viewModel => viewModel.Name))
                    .ForMember(entity => entity.Alias, map => map.MapFrom(viewModel => viewModel.Alias))
                    .ForMember(entity => entity.Order, map => map.MapFrom(viewModel => viewModel.Order));

                configuration.CreateMap<AddTestimonialViewModel, Testimonial>()
                    .ForMember(entity => entity.AuthorName, map => map.MapFrom(viewModel => viewModel.AuthorName))
                    .ForMember(entity => entity.AuthorImageFileId, map => map.MapFrom(viewModel => viewModel.AuthorImageFileId))
                    .ForMember(entity => entity.Text, map => map.MapFrom(viewModel => viewModel.Text));

                configuration.CreateMap<EditTestimonialViewModel, Testimonial>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(viewModel => viewModel.Id))
                    .ForMember(entity => entity.AuthorName, map => map.MapFrom(viewModel => viewModel.AuthorName))
                    .ForMember(entity => entity.AuthorImageFileId, map => map.MapFrom(viewModel => viewModel.AuthorImageFileId))
                    .ForMember(entity => entity.Text, map => map.MapFrom(viewModel => viewModel.Text));
            });

            mapper = mapperConfiguration.CreateMapper();
        }

        #region Entity в ViewModel

        internal static List<TViewModel> ToViewModelList<TEntity, TViewModel>(this IEnumerable<TEntity> entities, Func<TEntity, TViewModel> entityToViewModelExpression)
        {
            return entities == null ? null : entities.Select(entityToViewModelExpression).ToList();
        }

        internal static UserDataTableItemViewModel ToUserDataTableItemViewModel(this ApplicationUser applicationUser)
        {
            return mapper.Map<ApplicationUser, UserDataTableItemViewModel>(applicationUser);
        }

        internal static ImageFileDataTableItemViewModel ToImageFileDataTableItemViewModel(this ImageFile imageFile)
        {
            return mapper.Map<ImageFile, ImageFileDataTableItemViewModel>(imageFile);
        }

        internal static PhotographImageFileViewModel ToPhotographImageFileViewModel(this ImageFile imageFile)
        {
            return mapper.Map<ImageFile, PhotographImageFileViewModel>(imageFile);
        }

        internal static TestimonialAuthorImageFileViewModel ToTestimonialAuthorImageFileViewModel(this ImageFile imageFile)
        {
            return mapper.Map<ImageFile, TestimonialAuthorImageFileViewModel>(imageFile);
        }

        internal static UploadedImageFileViewModel ToUploadedImageFileViewModel(this ImageFile imageFile)
        {
            return mapper.Map<ImageFile, UploadedImageFileViewModel>(imageFile);
        }

        internal static MainMenuItemViewModel ToMainMenuItemViewModel(this MainMenuItem mainMenuItem)
        {
            return mapper.Map<MainMenuItem, MainMenuItemViewModel>(mainMenuItem);
        }

        internal static MainMenuItemDataTableItemViewModel ToMainMenuItemDataTableItemViewModel(this MainMenuItem mainMenuItem)
        {
            return mapper.Map<MainMenuItem, MainMenuItemDataTableItemViewModel>(mainMenuItem);
        }

        internal static PageDataTableItemViewModel ToPageDataTableItemViewModel(this Page page)
        {
            return mapper.Map<Page, PageDataTableItemViewModel>(page);
        }

        internal static SelectListItemViewModel ToSelectListItemViewModel(this PageTemplate pageTemplate)
        {
            return mapper.Map<PageTemplate, SelectListItemViewModel>(pageTemplate);
        }

        internal static PhotographViewModel ToPhotographViewModel(this Photograph photograph)
        {
            return mapper.Map<Photograph, PhotographViewModel>(photograph);
        }

        internal static PhotographDataTableItemViewModel ToPhotographDataTableItemViewModel(this Photograph photograph)
        {
            return mapper.Map<Photograph, PhotographDataTableItemViewModel>(photograph);
        }

        internal static PhotographCategoryViewModel ToPhotographCategoryViewModel(this PhotographCategory photographCategory)
        {
            return mapper.Map<PhotographCategory, PhotographCategoryViewModel>(photographCategory);
        }

        internal static PhotographCategoryDataTableItemViewModel ToPhotographCategoryDataTableItemViewModel(this PhotographCategory photographCategory)
        {
            return mapper.Map<PhotographCategory, PhotographCategoryDataTableItemViewModel>(photographCategory);
        }

        internal static TestimonialViewModel ToTestimonialViewModel(this Testimonial testimonial)
        {
            return mapper.Map<Testimonial, TestimonialViewModel>(testimonial);
        }

        internal static TestimonialDataTableItemViewModel ToTestimonialDataTableItemViewModel(this Testimonial testimonial)
        {
            return mapper.Map<Testimonial, TestimonialDataTableItemViewModel>(testimonial);
        }

        #endregion

        #region ViewModel в Entity

        internal static MainMenuItem ToMainMenuItem(this AddMainMenuItemViewModel addMainMenuItemViewModel)
        {
            return mapper.Map<AddMainMenuItemViewModel, MainMenuItem>(addMainMenuItemViewModel);
        }

        internal static MainMenuItem ToMainMenuItem(this EditMainMenuItemViewModel editMainMenuItemViewModel)
        {
            return mapper.Map<EditMainMenuItemViewModel, MainMenuItem>(editMainMenuItemViewModel);
        }

        internal static Page ToPage(this AddPageViewModel addPageViewModel)
        {
            return mapper.Map<AddPageViewModel, Page>(addPageViewModel);
        }

        internal static Page ToPage(this EditPageViewModel editPageViewModel)
        {
            return mapper.Map<EditPageViewModel, Page>(editPageViewModel);
        }

        internal static Photograph ToPhotograph(this AddPhotographViewModel addPhotographViewModel)
        {
            return mapper.Map<AddPhotographViewModel, Photograph>(addPhotographViewModel);
        }

        internal static Photograph ToPhotograph(this EditPhotographViewModel editPhotographViewModel)
        {
            return mapper.Map<EditPhotographViewModel, Photograph>(editPhotographViewModel);
        }

        internal static PhotographCategory ToPhotographCategory(this AddPhotographCategoryViewModel addPhotographCategoryViewModel)
        {
            return mapper.Map<AddPhotographCategoryViewModel, PhotographCategory>(addPhotographCategoryViewModel);
        }

        internal static PhotographCategory ToPhotographCategory(this EditPhotographCategoryViewModel editPhotographCategoryViewModel)
        {
            return mapper.Map<EditPhotographCategoryViewModel, PhotographCategory>(editPhotographCategoryViewModel);
        }

        internal static Testimonial ToTestimonial(this AddTestimonialViewModel addTestimonialViewModel)
        {
            return mapper.Map<AddTestimonialViewModel, Testimonial>(addTestimonialViewModel);
        }

        internal static Testimonial ToTestimonial(this EditTestimonialViewModel editTestimonialViewModel)
        {
            return mapper.Map<EditTestimonialViewModel, Testimonial>(editTestimonialViewModel);
        }

        #endregion

        private static string CombineUrls(string leftPart, string rightPart)
        {
            return new Uri(new Uri(leftPart), rightPart).ToString();
        }
    }
}