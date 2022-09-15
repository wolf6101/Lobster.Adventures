
using Lobster.Adventures.Domain.SeedWork;

namespace Lobster.Adventures.Domain.Entities
{
    public class AdventureNode : Entity
    {
        public AdventureNode(Guid id, Guid adventureId, string name) : base(id)
        {
            this.AdventureId = adventureId;
            this.Name = name;
        }
        public string Name { get; }
        public string Description { get; set; }
        public Guid AdventureId { get; }
        public Guid? ParentId { get; }
        public Guid? LeftChildId { get; }
        public Guid? RightChildId { get; }

        // Navigation Properties
        public AdventureNode? Parent { get; }
        public AdventureNode? LeftChild { get; }
        public AdventureNode? RightChild { get; }
    }
}