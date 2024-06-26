using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;
using ZemingoCMS.Infastructure.Data.EF.Repositories;

namespace ZemingoCMS.Infastructure.Data.EF
{
    public class EFUnitOfWork(CMSDbContext dbContext) : IUnitOfWork
    {
        public ICMSFieldsRepository CMSFieldsRepository { get; } = new EFCMSFieldsRepository(dbContext);

        public ICMSFieldValuesRepository CMSFieldsValuesRepository { get; } = new EFCMSFieldValuesRepository(dbContext);

        public ICMSItemsRepository CMSItemsRepository { get; } = new EFCMSItemsRepository(dbContext);

        public ICMSTypesRepository CMSTypesRepository { get; } = new EFCMSTypesRepository(dbContext);

        private readonly CMSDbContext _dbContext = dbContext;

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
