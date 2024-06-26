using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Infastructure.Data.EF.Mappings
{
    public class CMSTypeMap : IEntityTypeConfiguration<CMSType>
    {
        public void Configure(EntityTypeBuilder<CMSType> builder)
        {
            builder.ToTable("CMSTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(255);

            builder.HasMany(x => x.Fields).WithOne(x => x.Type).HasForeignKey(x => x.Id);
            builder.HasMany(x => x.Items).WithOne(x => x.Type).HasForeignKey(x => x.Id);
        }
    }
}
