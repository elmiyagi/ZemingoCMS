using ZemingoCMS.Application.Abstractions.Queries;
using ZemingoCMS.Application.CMS.Items.DTOs;
using ZemingoCMS.Application.CMS.Items.Mappings;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Items.Queries
{
    public sealed record GetCMSItemByIdQuery(Guid Id) : IQuery<CMSItemDTO?>
    {
    }

    public interface IGetCMSItemByIdQueryHandler : IQueryHandler<GetCMSItemByIdQuery, CMSItemDTO?>
    {
    }

    public sealed class GetCMSItemByIdQueryHandler(IRepository<CMSItem, Guid> repository)
        : IGetCMSItemByIdQueryHandler
    {
        private readonly IRepository<CMSItem, Guid> _repository = repository;

        public async Task<CMSItemDTO?> Handle(GetCMSItemByIdQuery query)
        {
            return (await _repository.GetById(query.Id))?.ToCMSItemDTO();
        }
    }
}
