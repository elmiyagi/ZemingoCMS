using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Infastructure.Data.EF.Repositories
{
    public class EFCMSItemsRepository(CMSDbContext dbContext) 
        : EFRepository<CMSItem, Guid>(dbContext), ICMSItemsRepository
    {
    }
}
