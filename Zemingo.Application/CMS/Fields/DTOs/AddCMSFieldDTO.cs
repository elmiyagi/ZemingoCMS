namespace ZemingoCMS.Application.CMS.Fields.DTOs
{
    public sealed record AddCMSFieldDTO(string Name, string FieldType, Guid CMSTypeId)
    {
    }
}
