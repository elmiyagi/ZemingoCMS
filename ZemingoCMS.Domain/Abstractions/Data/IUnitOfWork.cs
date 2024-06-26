namespace ZemingoCMS.Domain.Abstractions.Data
{
    public interface IUnitOfWork
    {
        ICMSFieldsRepository CMSFieldsRepository { get; }
        ICMSFieldValuesRepository CMSFieldsValuesRepository { get; }
        ICMSItemsRepository CMSItemsRepository { get; }
        ICMSTypesRepository CMSTypesRepository { get; }
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
