using Lobster.Adventures.Domain.Entities;

namespace Ardalis.GuardClauses
{
    public static class UserJourneyGuardExtensions
    {
        /// <summary>
        /// Throws InvalidOperationException on attempt to move status of actioned journey back to Created.
        /// </summary>
        /// <param name="newStatus"></param>
        /// <param name="oldStatus"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void MovingToCreatedFromInProgressStatus(this IGuardClause guardClause, UserJourneyStatusEnum newStatus, UserJourneyStatusEnum oldStatus)
        {
            if (newStatus == UserJourneyStatusEnum.Created && oldStatus > UserJourneyStatusEnum.Created)
            {
                throw new InvalidOperationException("Journey can't return to Created status after it was actioned");
            }
        }

        /// <summary>
        /// Throws TreeValidationException if first node of the path doesn't match root node of the tree.
        /// </summary>
        /// <param name="treeRootId"></param>
        /// <param name="pathFirstNodeId"></param>
        /// <param name="adventureId"></param>
        /// <exception cref="TreeValidationException"></exception>
        public static void InvalidPathFirstNode(this IGuardClause guardClause, Guid treeRootId, Guid pathFirstNodeId, Guid adventureId)
        {
            if (treeRootId != pathFirstNodeId)
            {
                throw new TreeValidationException($"Adventure path is invalid. Adventure '{adventureId}' doesn't have '{pathFirstNodeId} as a root node.'");
            }
        }

        /// <summary>
        /// Throws TreeValidationException if path contains leaf node in the middle.
        /// </summary>
        /// <param name="leafNodeIndex">Index of the leaf node in pathIds</param>
        /// <param name="pathIds">List of nodes from path</param>
        /// <exception cref="TreeValidationException"></exception>
        public static void DisconnectedPath(this IGuardClause guardClause, int leafNodeIndex, List<Guid> pathIds)
        {
            if (leafNodeIndex < pathIds.Count - 1)
            {
                throw new TreeValidationException($"Adventure path is invalid. Node '{pathIds[leafNodeIndex]}' is leaf, but path is not finished.");
            }
        }
    }
}