using Lobster.Adventures.Domain.SeedWork;

namespace Lobster.Adventures.Domain.Entities
{
    public class UserJourney : Entity
    {
        public UserJourney(Guid id) : base(id)
        {
        }
        public Guid AdventureId { get; }
        public Guid UserId { get; }
        public string Path { get; }
        public JourneyStatusEnum Status { get; }

        // Navigation Properties
        public User User { get; set; }
        public Adventure Adventure { get; set; }
    }
}