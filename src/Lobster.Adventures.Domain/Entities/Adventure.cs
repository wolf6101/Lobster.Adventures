using System.Collections.ObjectModel;

using Ardalis.GuardClauses;

using Lobster.Adventures.Domain.SeedWork;
namespace Lobster.Adventures.Domain.Entities
{
    public class Adventure : Entity
    {
        public Adventure(Guid id, string name, string? description = null) : base(id)
        {
            _name = name;
            _description = description;
        }

        private IList<AdventureNode> _nodes = new List<AdventureNode>();
        private IList<UserJourney> _journeys = new List<UserJourney>();
        private AdventureNode _rootNode;
        private int _numberOfNodes;
        private bool _treeConstructed;
        private Guid _rootNodeId;
        private string _name;
        private string? _description;

        public string Name { get => _name; private set => _name = value; }
        public string? Description { get => _description; private set => _description = value; }
        public int NumberOfNodes { get => _numberOfNodes; private set => _numberOfNodes = value; }
        public Guid RootNodeId { get => _rootNodeId; private set => _rootNodeId = value; }
        public IReadOnlyCollection<AdventureNode> Nodes => new ReadOnlyCollection<AdventureNode>(_nodes);
        public IReadOnlyCollection<UserJourney> Journeys => new ReadOnlyCollection<UserJourney>(_journeys);

        // Node by node insertion is not supported
        public void SetNodes(IList<AdventureNode> nodes)
        {
            Guard.Against.GreaterThan(_journeys.Count, 0, "Adventure tree can't be modified after it was actioned");

            var root = ConstructBinaryTree(nodes);

            AssertBinaryTreeIsValid(root, nodes);

            _rootNodeId = root.Id;
            _rootNode = root;
            _nodes = nodes;
            _numberOfNodes = nodes.Count;
            _treeConstructed = true;
        }

        public bool IsValid => Nodes.Count > 0 && RootNodeId != Guid.Empty;

        public AdventureNode GetRootNode()
        {
            if (_rootNodeId == Guid.Empty)
            {
                throw new InvalidOperationException("Adventure tree was not properly constructed yet");
            }

            if (_treeConstructed) return _rootNode;

            var root = ConstructBinaryTree(_nodes);
            _treeConstructed = true;

            return root;
        }

        /// <summary>
        /// Method constructs a tree by populating left and right nodes, based on plain node list input.
        /// Constructed tree migh have cycles or be disconnected. O(N) Time, O(N) Space complexity
        /// </summary>
        /// <param name="nodes">Plain list of nodes with children ids filled, but without LeftChild and RightChild properties filled</param>
        /// <returns>Root node of the tree</returns>
        private AdventureNode ConstructBinaryTree(IList<AdventureNode> nodes)
        {
            Guard.Against.NullOrEmpty(nodes, nameof(nodes));

            AdventureNode? root = null;

            var nodesDictionary = nodes.ToDictionary(k => k.Id, v => v); //O(N) T

            foreach (var node in nodes)
            {
                if (node.ParentId == null)
                {
                    root = Guard.Against.MultipleRootNodes(root, node);
                }
                else
                {
                    Guard.Against.InvalidNodeReference(nodesDictionary, (Guid)node.ParentId, node.Id);
                }

                if (node.LeftChildId != null)
                {
                    node.LeftChild = Guard.Against.InvalidNodeReference(nodesDictionary, (Guid)node.LeftChildId, node.Id);
                }

                if (node.RightChildId != null)
                {
                    node.RightChild = Guard.Against.InvalidNodeReference(nodesDictionary, (Guid)node.RightChildId, node.Id);
                }
            }

            Guard.Against.NullRootNode(root, this);

            return root;
        }

        /// <summary>
        /// Asserts that tree is valid by traversing it (linear DFS) from the root node.
        /// O(N) Time, O(N) Space complexity.
        /// </summary>
        /// <param name="nodes">List of nodes which represent a tree</param>
        /// <param name="root">Root of the tree</param>
        /// <exception cref="TreeValidationException"></exception>
        private static void AssertBinaryTreeIsValid(AdventureNode root, IList<AdventureNode> nodes)
        {
            var visitedNodes = new HashSet<Guid>();
            var nodeStack = new Stack<AdventureNode>();
            var parentIdStack = new Stack<Guid?>();

            AdventureNode? current = root;
            Guid? parentId = null;

            while (current != null || nodeStack.Count > 0)
            {
                while (current != null)
                {
                    visitedNodes.Add(Guard.Against.CyclicReference(visitedNodes, current.Id, parentId));

                    nodeStack.Push(current);
                    parentIdStack.Push(parentId);

                    parentId = current.Id;
                    current = current.LeftChild;
                }

                current = nodeStack.Pop();
                var callingParentId = parentIdStack.Pop();

                Guard.Against.InvalidChildToParentReference(current, callingParentId);

                parentId = current.Id;
                current = current.RightChild;
            }

            Guard.Against.NotConnectedTree(nodes, visitedNodes, root);
        }
    }
}