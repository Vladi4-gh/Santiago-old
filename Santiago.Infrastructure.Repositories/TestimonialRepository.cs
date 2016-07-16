using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Santiago.Core.Entities;
using Santiago.Core.Interfaces.Repositories;
using Santiago.Infrastructure.Repositories.DataProviding;
using Santiago.Infrastructure.Repositories.DataProviding.DbEntities;
using Santiago.Infrastructure.Repositories.Helpers;

namespace Santiago.Infrastructure.Repositories
{
    public class TestimonialRepository : ITestimonialRepository
    {
        public Testimonial GetTestimonialById(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbTestimonial>()
                    .Include(x => x.AuthorImageFile)
                    .SingleOrDefault(x => x.Id == id)
                    .ToTestimonial();
            }
        }

        public IEnumerable<Testimonial> GetAllTestimonialsOrderedByCreationDateDesc()
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbTestimonial>()
                    .Include(x => x.AuthorImageFile)
                    .OrderByDescending(x => x.CreationDate)
                    .ToList()
                    .Select(x => x.ToTestimonial());
            }
        }

        public IEnumerable<Testimonial> GetTestimonialsOrderedByCreationDateDesc(int skipNumber, int takeNumber)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbTestimonial>()
                    .Include(x => x.AuthorImageFile)
                    .OrderByDescending(x => x.CreationDate)
                    .Skip(skipNumber)
                    .Take(takeNumber)
                    .ToList()
                    .Select(x => x.ToTestimonial());
            }
        }

        public int GetTestimonialsTotalCount()
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                return context
                    .Set<DbTestimonial>()
                    .Count();
            }
        }

        public int CreateTestimonial(Testimonial testimonial)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbTestimonial = new DbTestimonial
                {
                    AuthorName = testimonial.AuthorName,
                    AuthorImageFileId = testimonial.AuthorImageFileId,
                    Text = testimonial.Text,
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                };

                context
                    .Set<DbTestimonial>()
                    .Add(dbTestimonial);

                context.SaveChanges();

                return dbTestimonial.Id;
            }
        }

        public void UpdateTestimonial(Testimonial testimonial)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var selectedDbTestimonial = context
                    .Set<DbTestimonial>()
                    .SingleOrDefault(x => x.Id == testimonial.Id);

                if (selectedDbTestimonial != null)
                {
                    selectedDbTestimonial.AuthorName = testimonial.AuthorName;
                    selectedDbTestimonial.AuthorImageFileId = testimonial.AuthorImageFileId;
                    selectedDbTestimonial.Text = testimonial.Text;
                    selectedDbTestimonial.LastModifiedDate = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteTestimonial(int id)
        {
            using (var context = ContextProvider.CreateNewContext())
            {
                var dbTestimonial = new DbTestimonial
                {
                    Id = id
                };

                context.Entry(dbTestimonial).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }
    }
}