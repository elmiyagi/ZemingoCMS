using Microsoft.EntityFrameworkCore;
using ZemingoCMS.Application.Abstractions.Queries;
using ZemingoCMS.Application.CMS.Types.DTOs;
using ZemingoCMS.Application.CMS.Types.Mappings;
using ZemingoCMS.Application.Models;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Types.Queries
{
    public sealed record GetCMSTypesQuery(PagedParams Params) : IQuery<List<CMSTypeDTO>>
    {
    }

    public interface IGetCMSTypesQueryHandler : IQueryHandler<GetCMSTypesQuery, List<CMSTypeDTO>>
    {
    }

    public sealed class GetCMSTypesQueryHandler(IRepository<CMSType, Guid> repository)
        : IGetCMSTypesQueryHandler
    {
        private readonly IRepository<CMSType, Guid> _repository = repository;

        public async Task<List<CMSTypeDTO>> Handle(GetCMSTypesQuery query)
        {
            return await _repository.Get().AsNoTracking()
                .Skip(query.Params.Page * query.Params.PageSize)
                .Take(query.Params.PageSize)
                .Select(x => x.ToCMSTypeDTO())
                .ToListAsync();
        }
    }
}
