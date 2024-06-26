namespace ZemingoCMS.Application.Models
{
    public sealed class ObjectCommandResult<T>(T result) : CommandResult() where T : class
    {
        public T Result { get; init; } = result;
    }
}
