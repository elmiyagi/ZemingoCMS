using ZemingoCMS.Application.CMS.Fields.DTOs;
using ZemingoCMS.Application.CMS.Items.DTOs;

namespace ZemingoCMS.Application.CMS.Types.DTOs
{
    public record CMSTypeDTO(Guid Id, string Name,
        List<CMSFieldDTO>? Fields = null, List<CMSItemDTO>? Items = null)
    {
    }
}
