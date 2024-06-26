using ZemingoCMS.Domain.Abstractions;

namespace ZemingoCMS.Domain.CMS.Entities
{
    public sealed class CMSItem : EntityBase<Guid>
    {
        public DateTime CreatedAt { get; init; }
        public CMSType Type { get; init; }
        public List<CMSFieldValue>? FieldValues { get ; init; }

        private CMSItem() { }

        public CMSItem(Guid id, DateTime createdAt, CMSType type, List<CMSFieldValue>? fieldValues = null) : base(id)
        {
            CreatedAt = createdAt;
            Type = type;
            FieldValues = fieldValues;
        }

        public static CMSItem Create(Guid id, DateTime createdAt, CMSType type, List<CMSFieldValue>? fieldValues = null)
        {
            return new CMSItem(id, createdAt, type, fieldValues);
        }
    }
}
