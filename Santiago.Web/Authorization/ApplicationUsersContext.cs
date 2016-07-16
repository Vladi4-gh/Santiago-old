using Microsoft.AspNet.Identity.EntityFramework;

namespace Santiago.Web.Authorization
{
    public class ApplicationUsersContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationUsersContext() : base("name=SantiagoConnection")
        {
        }

        public static ApplicationUsersContext Create()
        {
            return new ApplicationUsersContext();
        }
    }
}