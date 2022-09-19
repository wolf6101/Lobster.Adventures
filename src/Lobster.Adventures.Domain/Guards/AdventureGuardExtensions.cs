using Lobster.Adventures.Domain.Entities;

namespace Ardalis.GuardClauses
{
    //TODO: rework to rules
    public static class AdventureGuardExtensions
    {
        /// <summary>
        /// Throws TreeValidationException if root and rootCandidate are both not null
        /// </summary>
        /// <param name="root">Current tree root</param>
        /// <param name="rootCandidate">Root candidate</param>
        /// <returns>rootCandidate if root is null</returns>
        /// <exception cref="TreeValidationException"></exception>
        /// <exception cref="ArgumentNullException">When rootCandidate is null</exception>
        public static AdventureNode MultipleRootNodes(
            this IGuardClause guardClause,
            AdventureNode? root,
            AdventureNode rootCandidate)
        {
            if (rootCandidate == null)
            {
                throw new ArgumentNullException(nameof(rootCandidate));
            }

            if (root is not null)
            {
                throw new TreeValidationException(
                    $"Tree can't contain more than one root. Remove either '{root.Id}' or '{rootCandidate.Id}'.",
                    nameof(MultipleRootNodes));
            }

            return rootCandidate;
        }
        /// <summary>
        /// Throws TreeValidationException if referredNodeId is Guid.Empty or it is not present in the input dictionary.
        /// </summary>
        /// <param name="dict">Tree nodes dictionary</param>
        /// <param name="sourceNodeId">Node which references referredNodeId</param>
        /// <param name="referredNodeId">Node which is referenced by sourceNodeId</param>
        /// <returns>referredNode if found in the dict.</returns>
        /// <exception cref="TreeValidationException"></exception>
        public static AdventureNode InvalidNodeReference(this IGuardClause guardClause, IDictionary<Guid, AdventureNode> dict, Guid referredNodeId, Guid? sourceNodeId = null)
        {
            var isDefaultGuid = referredNodeId == Guid.Empty;

            if (isDefaultGuid || !dict.TryGetValue(referredNodeId, out var foundNode))
            {
                throw new TreeValidationException(
                    $"Node '{sourceNodeId}' references non existing '{referredNodeId}'.",
                    nameof(InvalidNodeReference));
            }

            return foundNode;
        }
        /// <summary>
        /// Throws TreeValidationException if root is null
        /// </summary>
        /// <param name="root">Root of the adventure tree</param>
        /// <param name="adventure">Adventure tree</param>
        /// <exception cref="TreeValidationException"></exception>
        public static void NullRootNode(this IGuardClause guardClause, AdventureNode? root, Adventure adventure)
        {
            if (root is null)
            {
                throw new TreeValidationException(
                    $"Adventure {adventure.Name} with Id: '{adventure.Id}' should have a root node",
                    nameof(NullRootNode));
            }
        }
        /// <summary>
        /// Throws TreeValidationException if node with nodeId was visited before
        /// </summary>
        /// <param name="visitedNodes">HashSet of already visited nodes</param>s
        /// <param name="nodeId">Guid of the node to be checked fo cyclic reference</param>
        /// <param name="parentId">Guid of the node that referenes nodeId</param>
        /// <returns>nodeId if node was not visited before</returns>
        /// <exception cref="Exception"></exception>
        public static Guid CyclicReference(this IGuardClause guardClause, HashSet<Guid> visitedNodes, Guid nodeId, Guid? parentId)
        {
            if (visitedNodes.TryGetValue(nodeId, out var foundId))
            {
                throw new TreeValidationException(
                    $"Cycles in the tree are not allowed. Node '{parentId}' creates a cycle with '{nodeId}'.",
                    nameof(CyclicReference));
            }

            return nodeId;
        }
        /// <summary>
        /// Throws TreeValidationException if node with callingParentId references node, which references different parent than callingParentId
        /// </summary>
        /// <param name="node">Node that is checked for wrong reference</param>
        /// <param name="callingParentId">Parent that references node</param>
        /// <exception cref="TreeValidationException"></exception>
        public static void InvalidChildToParentReference(this IGuardClause guardClause, AdventureNode node, Guid? callingParentId)
        {
            if (node.ParentId != callingParentId)
            {
                throw new TreeValidationException(
                    $"Broken child to parent reference. Node '{callingParentId}' references node '{node.Id}' as a child, while child references '{node.ParentId}' as a parent.",
                    nameof(InvalidChildToParentReference));
            }
        }
        /// <summary>
        /// Throws TreeValidationException if not all nodes in the nodes collection are reachable from the root node
        /// </summary>
        /// <param name="nodes">List of nodes representing binary tree</param>
        /// <param name="visitedNodes">Set of visited nodes</param>
        /// <param name="root">Root of the binary tree</param>
        /// <exception cref="TreeValidationException"></exception>
        public static void NotConnectedTree(this IGuardClause guardClause, IList<AdventureNode> nodes, HashSet<Guid> visitedNodes, AdventureNode root)
        {
            if (visitedNodes.Count != nodes.Count)
            {
                throw new TreeValidationException(
                    $"Tree represented as a list of {nameof(nodes)} is not connected. Not all nodes in the list are reachable from the root node '{root.Id}",
                    nameof(NotConnectedTree));
            }
        }
    }
}