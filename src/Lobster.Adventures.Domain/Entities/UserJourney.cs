using System;

using Lobster.Adventures.Domain.SeedWork;

namespace Lobster.Adventures.Domain.Entities
{
    public class UserJourney : Entity
    {
        private string _path;
        private UserJourneyStatusEnum _status;

        public UserJourney(Guid id, Guid adventureId, Guid userId) : base(id)
        {
            AdventureId = adventureId;
            UserId = userId;
            DateTimeCreated = DateTime.UtcNow;
            DateTimeUpdated = DateTime.UtcNow;
        }
        public Guid AdventureId { get; private set; }
        public Guid UserId { get; private set; }
        public string? Path
        {
            get => _path;
            set
            {
                AssertPathIsValid(_path);

                if (Status == UserJourneyStatusEnum.Created)
                {
                    Status = UserJourneyStatusEnum.InProgress;
                }
                _path = value;
                DateTimeUpdated = DateTime.UtcNow;
            }
        }

        public UserJourneyStatusEnum Status
        {
            get => _status;
            set
            {
                if (value == UserJourneyStatusEnum.Created && Status > UserJourneyStatusEnum.Created)
                {
                    throw new ArgumentException("Journey can't return to Created status after it was actioned");
                }

                _status = value;
                DateTimeUpdated = DateTime.UtcNow;
            }
        }
        public DateTime DateTimeCreated { get; private set; }
        public DateTime DateTimeUpdated { get; private set; }

        // Navigation Properties
        public User User { get; private set; }
        public Adventure Adventure { get; private set; }

        private void AssertPathIsValid(string path)
        {
            throw new NotImplementedException();
        }
    }
}