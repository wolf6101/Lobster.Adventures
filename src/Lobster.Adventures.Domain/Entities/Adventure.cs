using Lobster.Adventures.Domain.SeedWork;

namespace Lobster.Adventures.Domain.Entities
{
    public class Adventure : Entity
    {
        public Adventure(Guid id, string name, IEnumerable<AdventureNode> nodes) : base(id)
        {
            this.Name = name;
            Nodes = nodes;
        }
        public string Name { get; }
        public int Size { get; }
        public int Depth { get; }
        public Guid RootNodeId { get; }

        // Navigation Properties
        public AdventureNode RootNode { get; set; }
        public IEnumerable<AdventureNode> Nodes { get; }
    }
}