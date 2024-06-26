using ZemingoCMS.Domain.Abstractions;

namespace ZemingoCMS.Domain.CMS.Entities
{
    public sealed class CMSType : EntityBase<Guid>
    {
        public string Name { get; private set; }
        public List<CMSField>? Fields { get; init; }
        public List<CMSItem>? Items { get; init; }

        private CMSType() { }

        private CMSType(Guid id, string name, List<CMSField>? fields = null, List<CMSItem>? items = null) : base(id)
        {
            Name = name;
            Fields = fields;
            Items = items;
        }

        public static CMSType Create(Guid id, string name, List<CMSField>? fields = null, List<CMSItem>? items = null)
        {
            return new CMSType(id, name, fields, items);
        }

        public CMSType Update(string name)
        {
            Name = name;

            return this;
        }
    }
}
