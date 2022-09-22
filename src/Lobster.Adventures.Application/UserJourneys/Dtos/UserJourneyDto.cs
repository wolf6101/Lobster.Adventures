using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Domain.Entities;

namespace Lobster.Adventures.Application.UserJourneys.Dtos
{
    public class UserJourneyDto : IDto
    {
        public Guid Id { get; set; }
        public Guid AdventureId { get; set; }
        public Guid UserId { get; set; }
        public string? Path { get; set; }
        public UserJourneyStatusEnum Status { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}