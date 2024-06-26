using Microsoft.EntityFrameworkCore;
using ZemingoCMS.Application.Abstractions.Queries;
using ZemingoCMS.Application.CMS.Fields.DTOs;
using ZemingoCMS.Application.CMS.Fields.Mappings;
using ZemingoCMS.Application.Models;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Fields.Queries
{
    public sealed record GetCMSFieldsQuery(PagedParams Params) : IQuery<List<CMSFieldDTO>>
    {
    }

    public interface IGetCMSFieldsQueryHandler : IQueryHandler<GetCMSFieldsQuery, List<CMSFieldDTO>>
    {
    }

    public sealed class GetCMSFieldsQueryHandler(IRepository<CMSField, Guid> repository) : IGetCMSFieldsQueryHandler
    {
        private readonly IRepository<CMSField, Guid> _repository = repository;

        public async Task<List<CMSFieldDTO>> Handle(GetCMSFieldsQuery query)
        {
            return await _repository.Get().AsNoTracking()
                .Skip(query.Params.Page * query.Params.PageSize)
                .Take(query.Params.PageSize)
                .Select(x => x.ToCMSFieldDTO())
                .ToListAsync();
        }
    }
}
