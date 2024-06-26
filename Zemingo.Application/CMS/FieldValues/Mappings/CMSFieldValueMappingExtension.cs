using ZemingoCMS.Application.CMS.Fields.Mappings;
using ZemingoCMS.Application.CMS.FieldValues.DTOs;
using ZemingoCMS.Application.CMS.Items.Mappings;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.FieldValues.Mappings
{
    public static class CMSFieldValueMappingExtension
    {
        public static CMSFieldValueDTO ToCMSFieldValueDTO(this CMSFieldValue fieldValue)
        {
            return new CMSFieldValueDTO(fieldValue.Id, 
                fieldValue.Value, 
                fieldValue.Field.ToCMSFieldDTO(),
                fieldValue.Item.ToCMSItemDTO()
                );
        }

        public static CMSFieldValue ToCMSFieldValue(this CMSFieldValueDTO dto)
        {
            return CMSFieldValue.Create(dto.Id, dto.Value, dto.Field.ToCMSField(),dto.Item.ToCMSItem());
        }
    }
}
