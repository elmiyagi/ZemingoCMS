using Microsoft.EntityFrameworkCore;
using ZemingoCMS.Domain.Abstractions;
using ZemingoCMS.Domain.Abstractions.Data;

namespace ZemingoCMS.Infastructure.Data.EF
{
    public class EFRepository<TEntity, TId>(CMSDbContext dbContext) 
        : IRepository<TEntity, TId> where TEntity : EntityBase<TId>
    {
        private readonly CMSDbContext _dbContext = dbContext;

        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public IQueryable<TEntity> Get()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity?> GetById(TId id)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }
    }
}
