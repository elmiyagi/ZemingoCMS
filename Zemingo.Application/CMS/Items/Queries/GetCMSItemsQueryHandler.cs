using Microsoft.EntityFrameworkCore;
using ZemingoCMS.Application.Abstractions.Queries;
using ZemingoCMS.Application.CMS.Items.DTOs;
using ZemingoCMS.Application.CMS.Items.Mappings;
using ZemingoCMS.Application.Models;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Items.Queries
{
    public sealed record GetCMSItemsQuery(PagedParams Params) : IQuery<List<CMSItemDTO>>
    {
    }

    public interface IGetCMSItemsQueryHandler : IQueryHandler<GetCMSItemsQuery, List<CMSItemDTO>>
    {
    }

    public sealed class GetCMSItemsQueryHandler(IRepository<CMSItem, Guid> repository)
        : IGetCMSItemsQueryHandler
    {
        private readonly IRepository<CMSItem, Guid> _repository = repository;

        public async Task<List<CMSItemDTO>> Handle(GetCMSItemsQuery query)
        {
            return await _repository.Get().AsNoTracking()
                .Skip(query.Params.Page * query.Params.PageSize)
                .Take(query.Params.PageSize)
                .Select(x => x.ToCMSItemDTO())
                .ToListAsync();
        }
    }
}
