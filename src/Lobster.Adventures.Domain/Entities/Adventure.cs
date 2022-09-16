using Lobster.Adventures.Domain.SeedWork;

namespace Lobster.Adventures.Domain.Entities
{
    public class Adventure : Entity
    {
        public Adventure(Guid id, string name, string? description = null) : base(id)
        {
            this.Name = name;
            this.Description = description;
        }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public int NumberOfNodes { get; private set; }
        public int Depth { get; private set; }
        public Guid RootNodeId { get; private set; }

        // Navigation Properties
        public IEnumerable<AdventureNode> Nodes { get; private set; }
        public IEnumerable<UserJourney> Journeys { get; private set; }

        public void AddNodes(IList<AdventureNode> nodes)
        {
            this.Nodes = nodes;
            this.NumberOfNodes = nodes.Count;
            //TODO add depth calculation
            //TODO add root node calculation
            //TODO add validation
        }
    }
}