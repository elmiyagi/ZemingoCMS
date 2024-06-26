using ZemingoCMS.Application.CMS.Fields.Mappings;
using ZemingoCMS.Application.CMS.Items.Mappings;
using ZemingoCMS.Application.CMS.Types.DTOs;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Types.Mappings
{
    public static class CMSTypeMappingExtension
    {
        public static CMSTypeDTO ToCMSTypeDTO(this CMSType type)
        {
            return new CMSTypeDTO(type.Id, type.Name,
                type.Fields?.Select(x => x.ToCMSFieldDTO())?.ToList(),
                type.Items?.Select(x => x.ToCMSItemDTO())?.ToList());

        }

        public static CMSType ToCMSType(this CMSTypeDTO type)
        {
            return CMSType.Create(type.Id, type.Name, 
                type.Fields?.Select(x => x.ToCMSField()).ToList(),
                type.Items?.Select(x => x.ToCMSItem()).ToList());

        }
    }
}
