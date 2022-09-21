using System.ComponentModel.DataAnnotations;

using Lobster.Adventures.Application.SeedWork;

namespace Lobster.Adventures.Application.Adventures.Dtos
{
    public class CreateAdventureRequestDto : IDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<CreateAdventureNodeRequestDto> Nodes { get; set; }
    }
}