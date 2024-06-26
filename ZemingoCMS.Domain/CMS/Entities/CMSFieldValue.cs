using System.Xml.Linq;
using ZemingoCMS.Domain.Abstractions;

namespace ZemingoCMS.Domain.CMS.Entities
{
    public sealed class CMSFieldValue : EntityBase<Guid>
    {
        public string Value { get; init; }
        public CMSField Field { get; init; }
        public CMSItem Item { get; init; }

        private CMSFieldValue() { }

        private CMSFieldValue(Guid id, string value, CMSField field, CMSItem item) : base(id)
        {
            Value = value;
            Field = field;
            Item = item;
        }

        public static CMSFieldValue Create(Guid id, string value, CMSField field, CMSItem item)
        {
            return new CMSFieldValue(id, value, field, item);
        }
    }
}
