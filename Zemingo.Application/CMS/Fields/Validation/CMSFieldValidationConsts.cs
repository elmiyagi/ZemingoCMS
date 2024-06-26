using ZemingoCMS.Application.Models.Validation;

namespace ZemingoCMS.Application.CMS.Fields.Validation
{
    public static class CMSFieldValidationConsts
    {
        public const int NAME_MAX_LENGTH = 255;

        public static ValidationError NameLength() =>
            new ("CMS_FIELD_NAME_LENGTH",
                ValidationErrorType.InvalidProperty,
                $"CMS Field name length has to be between 1 and {NAME_MAX_LENGTH} chars");

        public static ValidationError NameAlreadyExists(string name) =>
            new("CMS_FIELD_NAME_ALREADY_EXISTS",
                ValidationErrorType.Conflict,
                $"CMS Field with name: {name} already exists");

        public static ValidationError NotFoundById(Guid id) =>
            new("CMS_FIELD_NOT_FOUND",
                ValidationErrorType.NotFound,
                $"CMS Field with id: {id} not found");
    }
}
