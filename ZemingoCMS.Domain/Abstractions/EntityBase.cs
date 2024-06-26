namespace ZemingoCMS.Domain.Abstractions
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; init; }

        protected EntityBase() { }

        protected EntityBase(TId id) 
        {
            Id = id;
        }
    }
}
