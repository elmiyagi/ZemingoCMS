using ZemingoCMS.Application.CMS.FieldValues.DTOs;
using ZemingoCMS.Application.CMS.Types.DTOs;

namespace ZemingoCMS.Application.CMS.Items.DTOs
{
    public record CMSItemDTO(Guid Id, DateTime CreatedAt, 
        CMSTypeDTO Type,
        List<CMSFieldValueDTO>? FieldValues = null)
    {
    }
}
