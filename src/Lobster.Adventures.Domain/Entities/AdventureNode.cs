
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
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid AdventureId { get; private set; }
        public Guid? ParentId { get; set; }
        public Guid? LeftChildId { get; set; }
        public Guid? RightChildId { get; set; }

        // Navigation Properties
        public Adventure Adventure { get; private set; }
    }
}