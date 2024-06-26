using ZemingoCMS.Application.CMS.FieldValues.Mappings;
using ZemingoCMS.Application.CMS.Items.DTOs;
using ZemingoCMS.Application.CMS.Types.Mappings;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Items.Mappings
{
    public static class CMSItemMappingExtension
    {
        public static CMSItemDTO ToCMSItemDTO(this CMSItem item)
        {
            return new CMSItemDTO(item.Id, item.CreatedAt,
                item.Type.ToCMSTypeDTO(),
                item.FieldValues?.Select(x => x.ToCMSFieldValueDTO()).ToList());
        }

        public static CMSItem ToCMSItem(this CMSItemDTO dto)
        {
            return CMSItem.Create(dto.Id, dto.CreatedAt,
                dto.Type.ToCMSType(),
                dto.FieldValues?.Select(x => x.ToCMSFieldValue()).ToList());
        }
    }
}
