namespace ZemingoCMS.Application.Models.Validation
{
    public sealed class ValidationResult
    {
        public bool IsSuccess { get; init; }
        public List<ValidationError> Errors { get; init; } = [];
        public ValidationResult() => IsSuccess = true;
        public ValidationResult(ValidationError error) => Errors.Add(error);
        public ValidationResult(List<ValidationError> errors) => Errors = errors;
    }
}
