using System.Data.Entity;
using Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DLayer.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<File> Files { get; set; }
        

    }
}
