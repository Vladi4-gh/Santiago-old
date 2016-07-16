using System.Data.Entity;
using Santiago.Infrastructure.Repositories.DataProviding.DbEntities;

namespace Santiago.Infrastructure.Repositories.DataProviding
{
    public class Context : DbContext
    {
        public Context() : base("name=SantiagoConnection")
        {
            Database.SetInitializer<Context>(null);
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbImageFile>();

            modelBuilder.Entity<DbMainMenuItem>();

            modelBuilder.Entity<DbPage>();

            modelBuilder.Entity<DbPageTemplate>();

            modelBuilder.Entity<DbPhotograph>();

            modelBuilder.Entity<DbPhotographCategory>();

            modelBuilder.Entity<DbSiteSetting>();

            modelBuilder.Entity<DbTestimonial>();
        }
    }
}