using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Domain.Abstractions.Data
{
    public interface ICMSItemsRepository : IRepository<CMSItem, Guid>
    {
    }
}
