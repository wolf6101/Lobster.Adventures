using Ardalis.GuardClauses;

using Lobster.Adventures.Domain.SeedWork;

namespace Lobster.Adventures.Domain.Entities
{
    public class UserJourney : Entity
    {
        public UserJourney(Guid id, Guid adventureId, Guid userId) : base(id)
        {
            AdventureId = adventureId;
            UserId = userId;
            DateTimeCreated = DateTime.UtcNow;
            DateTimeUpdated = DateTime.UtcNow;
        }
        public Guid AdventureId { get; private set; }
        public Guid UserId { get; private set; }
        public string? Path { get; private set; }
        public UserJourneyStatusEnum Status { get; private set; }
        public DateTime DateTimeCreated { get; private set; }
        public DateTime DateTimeUpdated { get; private set; }

        // Navigation Properties
        public User User { get; private set; }
        public Adventure Adventure { get; private set; }

        /// <summary>
        /// Sets complete (spans to the leaf) or incomplete path, if path exists in the tree.
        /// </summary>
        /// <param name="path">Path in the format ",Guid,Guid,Guid,"</param>
        public void SetPath(string path)
        {
            var root = Adventure.GetRootNode();

            AssertPathIsValid(path, root);

            Path = path;
            Status = UserJourneyStatusEnum.InProgress;
            DateTimeUpdated = DateTime.UtcNow;
        }

        public void SetStatus(UserJourneyStatusEnum newStatus)
        {
            if (newStatus == UserJourneyStatusEnum.Created && Status > UserJourneyStatusEnum.Created)
            {
                throw new InvalidOperationException("Journey can't return to Created status after it was actioned");
            }

            Status = newStatus;
            DateTimeUpdated = DateTime.UtcNow;
        }
        private void AssertPathIsValid(string path, AdventureNode root)
        {
            Guard.Against.NullOrEmpty(path, nameof(path));
            Guard.Against.Null(root, nameof(root));

            var currentIndex = 0;
            var pathIds = path.Split(',')
                              .Where(p => !string.IsNullOrEmpty(p))
                              .Select(id => new Guid(id))
                              .ToList();

            AdventureNode? current = root;

            if (current.Id != pathIds[currentIndex])
            {
                throw new TreeValidationException($"Adventure path is invalid. Adventure '{Adventure.Id}' doesn't have '{pathIds[currentIndex]} as a root node.'");
            }

            while (currentIndex < pathIds.Count - 1)
            {
                if (current.LeftChildId == null && current.RightChild == null)
                {
                    if (currentIndex < pathIds.Count)
                    {
                        throw new TreeValidationException($"Adventure path is invalid. Node '{pathIds[currentIndex]}' has no more children, but path is not finished.");
                    }
                    return;
                }

                if (current.LeftChildId == pathIds[currentIndex + 1])
                {
                    current = current.LeftChild;
                    currentIndex++;
                }
                else if (current.RightChildId == pathIds[currentIndex + 1])
                {
                    current = current.RightChild;
                    currentIndex++;
                }
                else
                {
                    throw new TreeValidationException($"Adventure path is invalid. Adventure '{Adventure.Id}' doesn't have '{pathIds[currentIndex + 1]}' node in it.");
                }
            }
        }
    }
}