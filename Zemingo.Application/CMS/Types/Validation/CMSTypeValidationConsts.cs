using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZemingoCMS.Application.Models.Validation;

namespace ZemingoCMS.Application.CMS.Types.Validation
{
    public static class CMSTypeValidationConsts
    {
        public const int NAME_MAX_LENGTH = 255;

        public static ValidationError NameLength() =>
            new("CMS_TYPE_NAME_LENGTH",
                ValidationErrorType.InvalidProperty,
                $"CMS Type name length has to be between 1 and {NAME_MAX_LENGTH} chars");

        public static ValidationError NameAlreadyExists(string name) =>
            new("CMS_TYPE_NAME_ALREADY_EXISTS",
                ValidationErrorType.Conflict,
                $"CMS Type with name: {name} already exists");

        public static ValidationError NotFoundById(Guid id) =>
            new("CMS_TYPE_NOT_FOUND",
                ValidationErrorType.NotFound,
                $"CMS Type with id: {id} not found");
    }
}
