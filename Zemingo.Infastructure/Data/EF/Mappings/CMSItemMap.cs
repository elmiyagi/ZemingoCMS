using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Infastructure.Data.EF.Mappings
{
    public class CMSItemMap : IEntityTypeConfiguration<CMSItem>
    {
        public void Configure(EntityTypeBuilder<CMSItem> builder)
        {
            builder.ToTable("CMSItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt);

            builder.HasMany(x => x.FieldValues).WithOne(x => x.Item).HasForeignKey(x => x.Id);
        }
    }
}
