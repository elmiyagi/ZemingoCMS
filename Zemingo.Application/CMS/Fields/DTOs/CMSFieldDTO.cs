using ZemingoCMS.Application.CMS.FieldValues.DTOs;
using ZemingoCMS.Application.CMS.Types.DTOs;

namespace ZemingoCMS.Application.CMS.Fields.DTOs
{
    public record CMSFieldDTO(Guid Id, string Name, string FieldType,
        CMSTypeDTO Type,
        List<CMSFieldValueDTO>? FieldValues = null)
    {
    }
}
