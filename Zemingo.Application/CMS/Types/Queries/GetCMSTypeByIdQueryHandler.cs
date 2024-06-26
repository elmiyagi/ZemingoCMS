using ZemingoCMS.Application.Abstractions.Queries;
using ZemingoCMS.Application.CMS.Types.DTOs;
using ZemingoCMS.Application.CMS.Types.Mappings;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Types.Queries
{
    public sealed record GetCMSTypeByIdQuery(Guid Id) : IQuery<CMSTypeDTO?>
    {
    }

    public interface IGetCMSTypeByIdQueryHandler : IQueryHandler<GetCMSTypeByIdQuery, CMSTypeDTO?>
    {
    }

    public sealed class GetCMSTypeByIdQueryHandler(IRepository<CMSType, Guid> repository)
        : IGetCMSTypeByIdQueryHandler
    {
        private readonly IRepository<CMSType, Guid> _repository = repository;

        public async Task<CMSTypeDTO?> Handle(GetCMSTypeByIdQuery query)
        {
            return (await _repository.GetById(query.Id))?.ToCMSTypeDTO();
        }
    }
}
