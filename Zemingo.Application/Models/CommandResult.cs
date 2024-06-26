using ZemingoCMS.Application.Models.Validation;

namespace ZemingoCMS.Application.Models
{
    public class CommandResult
    {
        public bool IsSuccess { get; init; }
        public List<ValidationError> Errors { get; init; } = [];
        public CommandResult() => IsSuccess = true;
        public CommandResult(ValidationError error)
        {
            Errors.Add(error);
        }

        public CommandResult(List<ValidationError> errors)
        {
            Errors = errors;
        }
    }
}
