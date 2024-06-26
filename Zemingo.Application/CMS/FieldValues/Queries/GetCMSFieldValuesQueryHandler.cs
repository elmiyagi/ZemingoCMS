using Microsoft.EntityFrameworkCore;
using ZemingoCMS.Application.Abstractions.Queries;
using ZemingoCMS.Application.CMS.FieldValues.DTOs;
using ZemingoCMS.Application.CMS.FieldValues.Mappings;
using ZemingoCMS.Application.Models;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.FieldValues.Queries
{
    public sealed record GetCMSFieldValuesQuery(PagedParams Params) : IQuery<List<CMSFieldValueDTO>>
    {
    }

    public interface IGetCMSFieldValuesQueryHandler : IQueryHandler<GetCMSFieldValuesQuery, List<CMSFieldValueDTO>>
    {
    }

    public sealed class GetCMSFieldValuesQueryHandler(IRepository<CMSFieldValue, Guid> repository) 
        : IGetCMSFieldValuesQueryHandler
    {
        private readonly IRepository<CMSFieldValue, Guid> _repository = repository;

        public async Task<List<CMSFieldValueDTO>> Handle(GetCMSFieldValuesQuery query)
        {
            return await _repository.Get().AsNoTracking()
                .Skip(query.Params.Page * query.Params.PageSize)
                .Take(query.Params.PageSize)
                .Select(x => x.ToCMSFieldValueDTO())
                .ToListAsync();
        }
    }
}
