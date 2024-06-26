using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Infastructure.Data.EF.Mappings
{
    public class CMSFieldMap : IEntityTypeConfiguration<CMSField>
    {
        public void Configure(EntityTypeBuilder<CMSField> builder)
        {
            builder.ToTable("CMSFields");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(255);
            builder.Property(x => x.FieldType).HasMaxLength(255);

            builder.HasMany(x => x.FieldValues).WithOne(x => x.Field).HasForeignKey(x => x.Id);
        }
    }
}
