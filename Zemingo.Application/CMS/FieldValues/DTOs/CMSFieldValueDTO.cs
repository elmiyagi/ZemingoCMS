using ZemingoCMS.Application.CMS.Fields.DTOs;
using ZemingoCMS.Application.CMS.Items.DTOs;

namespace ZemingoCMS.Application.CMS.FieldValues.DTOs
{
    public record CMSFieldValueDTO(Guid Id, string Value, CMSFieldDTO Field, CMSItemDTO Item)
    {
    }
}
