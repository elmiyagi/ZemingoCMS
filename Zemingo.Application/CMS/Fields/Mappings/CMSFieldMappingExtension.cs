using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZemingoCMS.Application.CMS.Fields.DTOs;
using ZemingoCMS.Application.CMS.FieldValues.Mappings;
using ZemingoCMS.Application.CMS.Types.Mappings;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Fields.Mappings
{
    public static class CMSFieldMappingExtension
    {
        public static CMSFieldDTO ToCMSFieldDTO(this CMSField field)
        {
            return new CMSFieldDTO(field.Id, field.Name, field.FieldType,
                field.Type.ToCMSTypeDTO(),
                field.FieldValues?.Select(x => x.ToCMSFieldValueDTO()).ToList());
        }

        public static CMSField ToCMSField(this CMSFieldDTO field)
        {
            return CMSField.Create(field.Id,
                field.Name, 
                field.FieldType, 
                field.Type.ToCMSType(), 
                field.FieldValues?.Select(x => x.ToCMSFieldValue()).ToList());
        }
    }
}
