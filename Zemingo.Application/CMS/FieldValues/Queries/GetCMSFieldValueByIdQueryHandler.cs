using ZemingoCMS.Application.Abstractions.Queries;
using ZemingoCMS.Application.CMS.Fields.DTOs;
using ZemingoCMS.Application.CMS.FieldValues.DTOs;
using ZemingoCMS.Application.CMS.FieldValues.Mappings;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.FieldValues.Queries
{
    public sealed record GetCMSFieldValueByIdQuery(Guid Id) : IQuery<CMSFieldValueDTO?>
    {
    }

    public interface IGetCMSFieldValueByIdQueryHandler : IQueryHandler<GetCMSFieldValueByIdQuery, CMSFieldValueDTO?>
    {
    }

    public sealed class GetCMSFieldValueByIdQueryHandler(IRepository<CMSFieldValue, Guid> repository)
        : IGetCMSFieldValueByIdQueryHandler
    {
        private readonly IRepository<CMSFieldValue, Guid> _repository = repository;

        public async Task<CMSFieldValueDTO?> Handle(GetCMSFieldValueByIdQuery query)
        {
            return (await _repository.GetById(query.Id))?.ToCMSFieldValueDTO();
        }
    }
}
