using ZemingoCMS.Application.Abstractions.Queries;
using ZemingoCMS.Application.CMS.Fields.DTOs;
using ZemingoCMS.Application.CMS.Fields.Mappings;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Fields.Queries
{
    public sealed record GetCMSFieldByIdQuery(Guid Id) : IQuery<CMSFieldDTO?>
    {
    }

    public interface IGetCMSFieldByIdQueryHandler : IQueryHandler<GetCMSFieldByIdQuery, CMSFieldDTO?>
    {
    }

    public sealed class GetCMSFieldByIdQueryHandler(IRepository<CMSField, Guid> repository) 
        : IGetCMSFieldByIdQueryHandler
    {
        private readonly IRepository<CMSField, Guid> _repository = repository;

        public async Task<CMSFieldDTO?> Handle(GetCMSFieldByIdQuery query)
        {
            return (await _repository.GetById(query.Id))?.ToCMSFieldDTO(); 
        }
    }
}
