using System.ComponentModel.DataAnnotations;

using Lobster.Adventures.Application.SeedWork;

namespace Lobster.Adventures.Application.Adventures.Dtos
{
    public class CreateAdventureNodeRequestDto : IDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? LeftChildId { get; set; }
        public Guid? RightChildId { get; set; }
    }
}