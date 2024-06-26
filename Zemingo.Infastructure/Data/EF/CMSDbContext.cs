using Microsoft.EntityFrameworkCore;
using ZemingoCMS.Infastructure.Data.EF.Mappings;

namespace ZemingoCMS.Infastructure.Data.EF
{
    public class CMSDbContext : DbContext
    {
        public CMSDbContext()
        {

        }

        public CMSDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CMSFieldMap).Assembly);
        }
    }
}
