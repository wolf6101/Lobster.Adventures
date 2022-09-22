using System.ComponentModel.DataAnnotations;

using Lobster.Adventures.Application.SeedWork;

namespace Lobster.Adventures.Application.UserJourneys.Dtos
{
    public class CreateUserJourneyRequestDto : IDto
    {
        [Required]
        public Guid AdventureId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public string? Path { get; set; }
    }
}