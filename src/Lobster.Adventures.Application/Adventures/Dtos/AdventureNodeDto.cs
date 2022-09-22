using Lobster.Adventures.Application.SeedWork;

namespace Lobster.Adventures.Application.Adventures.Dtos
{
    public class AdventureNodeDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid AdventureId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? LeftChildId { get; set; }
        public Guid? RightChildId { get; set; }
    }
}