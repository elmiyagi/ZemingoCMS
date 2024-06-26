using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Infastructure.Data.EF.Repositories
{
    public class EFCMSTypesRepository(CMSDbContext dbContext) 
        : EFRepository<CMSType, Guid>(dbContext), ICMSTypesRepository
    {
    }
}
