using ZemingoCMS.Domain.Abstractions;

namespace ZemingoCMS.Domain.CMS.Entities
{
    public sealed class CMSField : EntityBase<Guid>
    {
        public string Name { get; private set; }
        public string FieldType { get; private set; }
        public CMSType Type { get; init; }
        public List<CMSFieldValue>? FieldValues { get; init; }

        private CMSField(){ }

        private CMSField(Guid id, string name, string fieldType, CMSType type, List<CMSFieldValue>? fieldValues = null) : base(id)
        {
            Name = name;
            FieldType = fieldType;
            Type = type;
            FieldValues = fieldValues;
        }

        public CMSField Update(string name, string fieldType)
        {
            Name = name;
            FieldType = fieldType;

            return this;
        }

        public static CMSField Create(Guid id, string name, string fieldType, CMSType type, List<CMSFieldValue>? fieldValues = null)
        {
            return new CMSField(id, name, fieldType, type, fieldValues);
        }
    }
}
