namespace ZemingoCMS.Domain.Abstractions.Data
{
    public interface IRepository<TEntity, TId> where TEntity : EntityBase<TId>
    {
        IQueryable<TEntity> Get();
        Task<TEntity?> GetById(TId id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
