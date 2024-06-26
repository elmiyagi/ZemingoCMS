using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Infastructure.Data.EF.Repositories
{
    public class EFCMSFieldsRepository(CMSDbContext dbContext) 
        : EFRepository<CMSField, Guid>(dbContext), ICMSFieldsRepository
    {
    }
}
