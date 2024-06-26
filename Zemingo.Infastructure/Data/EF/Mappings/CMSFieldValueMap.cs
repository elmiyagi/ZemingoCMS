using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Infastructure.Data.EF.Mappings
{
    public class CMSFieldValueMap : IEntityTypeConfiguration<CMSFieldValue>
    {
        public void Configure(EntityTypeBuilder<CMSFieldValue> builder)
        {
            builder.ToTable("CMSFieldValues");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Value);
        }
    }
}
