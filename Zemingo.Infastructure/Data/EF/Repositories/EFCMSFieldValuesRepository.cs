using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Infastructure.Data.EF.Repositories
{
    public class EFCMSFieldValuesRepository(CMSDbContext dbContext) 
        : EFRepository<CMSFieldValue, Guid>(dbContext), ICMSFieldValuesRepository
    {
    }
}
