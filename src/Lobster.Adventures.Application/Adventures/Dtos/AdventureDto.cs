using Lobster.Adventures.Application.SeedWork;

namespace Lobster.Adventures.Application.Adventures.Dtos
{
    public class AdventureDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfNodes { get; set; }
        public Guid RootNodeId { get; set; }
        public List<AdventureNodeDto> Nodes { get; set; }
    }
}