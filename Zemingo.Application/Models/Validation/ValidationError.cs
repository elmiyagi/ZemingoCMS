namespace ZemingoCMS.Application.Models.Validation
{
    public sealed record ValidationError(string Code, ValidationErrorType Type, string Message)
    {
    }
}
