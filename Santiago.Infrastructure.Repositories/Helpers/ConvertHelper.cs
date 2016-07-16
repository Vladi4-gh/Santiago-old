using AutoMapper;
using Santiago.Core.Entities;
using Santiago.Infrastructure.Repositories.DataProviding.DbEntities;

namespace Santiago.Infrastructure.Repositories.Helpers
{
    internal static class ConvertHelper
    {
        private static readonly IMapper mapper;

        static ConvertHelper()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<DbImageFile, ImageFile>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(dbEntity => dbEntity.Id))
                    .ForMember(entity => entity.Name, map => map.MapFrom(dbEntity => dbEntity.Name))
                    .ForMember(entity => entity.Length, map => map.MapFrom(dbEntity => dbEntity.Length))
                    .ForMember(entity => entity.Width, map => map.MapFrom(dbEntity => dbEntity.Width))
                    .ForMember(entity => entity.Height, map => map.MapFrom(dbEntity => dbEntity.Height))
                    .ForMember(entity => entity.UploadDate, map => map.MapFrom(dbEntity => dbEntity.UploadDate));

                configuration.CreateMap<DbMainMenuItem, MainMenuItem>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(dbEntity => dbEntity.Id))
                    .ForMember(entity => entity.Text, map => map.MapFrom(dbEntity => dbEntity.Text))
                    .ForMember(entity => entity.Url, map => map.MapFrom(dbEntity => dbEntity.Url))
                    .ForMember(entity => entity.Order, map => map.MapFrom(dbEntity => dbEntity.Order))
                    .ForMember(entity => entity.CreationDate, map => map.MapFrom(dbEntity => dbEntity.CreationDate))
                    .ForMember(entity => entity.LastModifiedDate, map => map.MapFrom(dbEntity => dbEntity.LastModifiedDate));

                configuration.CreateMap<DbPage, Page>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(dbEntity => dbEntity.Id))
                    .ForMember(entity => entity.Title, map => map.MapFrom(dbEntity => dbEntity.Title))
                    .ForMember(entity => entity.Alias, map => map.MapFrom(dbEntity => dbEntity.Alias))
                    .ForMember(entity => entity.Text, map => map.MapFrom(dbEntity => dbEntity.Text))
                    .ForMember(entity => entity.MetaDescription, map => map.MapFrom(dbEntity => dbEntity.MetaDescription))
                    .ForMember(entity => entity.MetaKeywords, map => map.MapFrom(dbEntity => dbEntity.MetaKeywords))
                    .ForMember(entity => entity.ParentId, map => map.MapFrom(dbEntity => dbEntity.ParentId))
                    .ForMember(entity => entity.Parent, map => map.MapFrom(dbEntity => dbEntity.Parent))
                    .ForMember(entity => entity.SitemapXmlChangeFrequency, map => map.MapFrom(dbEntity => dbEntity.SitemapXmlChangeFrequency))
                    .ForMember(entity => entity.SitemapXmlPriority, map => map.MapFrom(dbEntity => dbEntity.SitemapXmlPriority))
                    .ForMember(entity => entity.TemplateId, map => map.MapFrom(dbEntity => dbEntity.TemplateId))
                    .ForMember(entity => entity.Template, map => map.MapFrom(dbEntity => dbEntity.Template))
                    .ForMember(entity => entity.IsPublished, map => map.MapFrom(dbEntity => dbEntity.IsPublished))
                    .ForMember(entity => entity.CreationDate, map => map.MapFrom(dbEntity => dbEntity.CreationDate))
                    .ForMember(entity => entity.LastModifiedDate, map => map.MapFrom(dbEntity => dbEntity.LastModifiedDate))
                    .ForMember(entity => entity.PublicationDate, map => map.MapFrom(dbEntity => dbEntity.PublicationDate));

                configuration.CreateMap<DbPageTemplate, PageTemplate>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(dbEntity => dbEntity.Id))
                    .ForMember(entity => entity.Title, map => map.MapFrom(dbEntity => dbEntity.Title))
                    .ForMember(entity => entity.ControllerName, map => map.MapFrom(dbEntity => dbEntity.ControllerName))
                    .ForMember(entity => entity.ActionName, map => map.MapFrom(dbEntity => dbEntity.ActionName));

                configuration.CreateMap<DbPhotograph, Photograph>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(dbEntity => dbEntity.Id))
                    .ForMember(entity => entity.Title, map => map.MapFrom(dbEntity => dbEntity.Title))
                    .ForMember(entity => entity.Description, map => map.MapFrom(dbEntity => dbEntity.Description))
                    .ForMember(entity => entity.CategoryId, map => map.MapFrom(dbEntity => dbEntity.CategoryId))
                    .ForMember(entity => entity.Category, map => map.MapFrom(dbEntity => dbEntity.Category))
                    .ForMember(entity => entity.GalleryItemImageFileId, map => map.MapFrom(dbEntity => dbEntity.GalleryItemImageFileId))
                    .ForMember(entity => entity.GalleryItemImageFile, map => map.MapFrom(dbEntity => dbEntity.GalleryItemImageFile))
                    .ForMember(entity => entity.GallerySliderImageFileId, map => map.MapFrom(dbEntity => dbEntity.GallerySliderImageFileId))
                    .ForMember(entity => entity.GallerySliderImageFile, map => map.MapFrom(dbEntity => dbEntity.GallerySliderImageFile))
                    .ForMember(entity => entity.CreationDate, map => map.MapFrom(dbEntity => dbEntity.CreationDate))
                    .ForMember(entity => entity.LastModifiedDate, map => map.MapFrom(dbEntity => dbEntity.LastModifiedDate));

                configuration.CreateMap<DbPhotographCategory, PhotographCategory>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(dbEntity => dbEntity.Id))
                    .ForMember(entity => entity.Name, map => map.MapFrom(dbEntity => dbEntity.Name))
                    .ForMember(entity => entity.Alias, map => map.MapFrom(dbEntity => dbEntity.Alias))
                    .ForMember(entity => entity.Order, map => map.MapFrom(dbEntity => dbEntity.Order))
                    .ForMember(entity => entity.CreationDate, map => map.MapFrom(dbEntity => dbEntity.CreationDate))
                    .ForMember(entity => entity.LastModifiedDate, map => map.MapFrom(dbEntity => dbEntity.LastModifiedDate));

                configuration.CreateMap<DbSiteSetting, SiteSetting>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(dbEntity => dbEntity.Id))
                    .ForMember(entity => entity.Name, map => map.MapFrom(dbEntity => dbEntity.Name))
                    .ForMember(entity => entity.Value, map => map.MapFrom(dbEntity => dbEntity.Value));

                configuration.CreateMap<DbTestimonial, Testimonial>()
                    .ForMember(entity => entity.Id, map => map.MapFrom(dbEntity => dbEntity.Id))
                    .ForMember(entity => entity.AuthorName, map => map.MapFrom(dbEntity => dbEntity.AuthorName))
                    .ForMember(entity => entity.AuthorImageFileId, map => map.MapFrom(dbEntity => dbEntity.AuthorImageFileId))
                    .ForMember(entity => entity.AuthorImageFile, map => map.MapFrom(dbEntity => dbEntity.AuthorImageFile))
                    .ForMember(entity => entity.Text, map => map.MapFrom(dbEntity => dbEntity.Text))
                    .ForMember(entity => entity.CreationDate, map => map.MapFrom(dbEntity => dbEntity.CreationDate))
                    .ForMember(entity => entity.LastModifiedDate, map => map.MapFrom(dbEntity => dbEntity.LastModifiedDate));
            });

            mapper = mapperConfiguration.CreateMapper();
        }

        internal static ImageFile ToImageFile(this DbImageFile dbImageFile)
        {
            return mapper.Map<DbImageFile, ImageFile>(dbImageFile);
        }

        internal static MainMenuItem ToMainMenuItem(this DbMainMenuItem dbMainMenuItem)
        {
            return mapper.Map<DbMainMenuItem, MainMenuItem>(dbMainMenuItem);
        }

        internal static Page ToPage(this DbPage dbPage)
        {
            return mapper.Map<DbPage, Page>(dbPage);
        }

        internal static PageTemplate ToPageTemplate(this DbPageTemplate dbPageTemplate)
        {
            return mapper.Map<DbPageTemplate, PageTemplate>(dbPageTemplate);
        }

        internal static Photograph ToPhotograph(this DbPhotograph dbPhotograph)
        {
            return mapper.Map<DbPhotograph, Photograph>(dbPhotograph);
        }

        internal static PhotographCategory ToPhotographCategory(this DbPhotographCategory dbPhotographCategory)
        {
            return mapper.Map<DbPhotographCategory, PhotographCategory>(dbPhotographCategory);
        }

        internal static SiteSetting ToSiteSetting(this DbSiteSetting dbSiteSetting)
        {
            return mapper.Map<DbSiteSetting, SiteSetting>(dbSiteSetting);
        }

        internal static Testimonial ToTestimonial(this DbTestimonial dbTestimonial)
        {
            return mapper.Map<DbTestimonial, Testimonial>(dbTestimonial);
        }
    }
}