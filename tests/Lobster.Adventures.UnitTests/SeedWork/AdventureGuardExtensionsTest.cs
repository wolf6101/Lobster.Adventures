using System;
using System.Collections.Generic;
using System.Linq;

using Ardalis.GuardClauses;

using Lobster.Adventures.Domain.Entities;
using Lobster.Adventures.UnitTests.Domain.TestDataProviders;

using Xunit;

namespace Lobster.Adventures.UnitTests.Domain
{
    public class AdventureGuardExtensionsTest
    {
        [Fact]
        public void MultipleRootNodesGuard_InvalidInput_ShouldThrowArgumentNullException()
        {
            // Arrange
            var node = new AdventureNode(Guid.Empty, Guid.Empty, "name");
            AdventureNode nullNode = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => Guard.Against.MultipleRootNodes(null, nullNode));
            Assert.Throws<ArgumentNullException>(() => Guard.Against.MultipleRootNodes(node, nullNode));
        }

        [Fact]
        public void MultipleRootNodesGuard_RootAlreadyExist_ShouldThrowTreeValidationException()
        {
            // Arrange
            var root = new AdventureNode(new Guid("213bedd9-92ce-4db7-a67c-0c3e4f3fc952"), Guid.Empty, "root");
            var rootCandidate = new AdventureNode(new Guid("5c9b8041-dbbf-4665-9b0d-12a8e6890a0e"), Guid.Empty, "candidate");

            // Assert
            Assert.Throws<TreeValidationException>(() => Guard.Against.MultipleRootNodes(root, rootCandidate));
        }

        [Fact]
        public void MultipleRootNodesGuard_RootDoesNotExist_ShouldReturnCandidate()
        {
            // Arrange
            AdventureNode root = null;
            var rootCandidate = new AdventureNode(new Guid("5c9b8041-dbbf-4665-9b0d-12a8e6890a0e"), Guid.Empty, "candidate");

            // Act
            var result = Guard.Against.MultipleRootNodes(root, rootCandidate);

            // Assert
            Assert.Equal(result, rootCandidate);
        }

        [Fact]
        public void InvalidNodeReferenceGuard_NodeDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var nodes = AdventureTestDataProvider.GetAdventureNodes(new Guid("e85f2d52-fcd9-4a84-afd6-a51fdb62396e"));
            var nodeWithWrongLeftRef = new AdventureNode(new Guid("be59a543-a1e9-49bd-8bf4-0c31478ecbd3"), Guid.Empty, "Node")
            {
                LeftChildId = new Guid("08a9f387-78e9-4b49-966f-5284868ca13c")
            };

            nodes.Add(nodeWithWrongLeftRef);

            var nodesDictionary = nodes.ToDictionary(k => k.Id, v => v);

            // Assert
            Assert.Throws<TreeValidationException>(() =>
                Guard.Against.InvalidNodeReference(nodesDictionary, (Guid)nodeWithWrongLeftRef.LeftChildId, (Guid)nodeWithWrongLeftRef.Id));
        }

        [Fact]
        public void InvalidNodeReferenceGuard_NodeReferenceOK_ShouldReturnReferredNode()
        {
            // Arrange
            var nodes = AdventureTestDataProvider.GetAdventureNodes(new Guid("e85f2d52-fcd9-4a84-afd6-a51fdb62396e"));

            var nodesDictionary = nodes.ToDictionary(k => k.Id, v => v); //O(N) T
            var first = nodes.First();

            // Act
            var leftChild = Guard.Against.InvalidNodeReference(nodesDictionary, (Guid)first.LeftChildId, first.Id);

            // Assert
            Assert.Equal(leftChild.Id, first.LeftChildId);
        }

        [Fact]
        public void NullRootNodeGuard_RootIsNool_ShouldThrowException()
        {
            // Arrange
            var adventure = AdventureTestDataProvider.GetAdventureWithoutNodes(new Guid("7e67bfaf-4c85-4a44-afc5-4e46b6b085e1"));

            AdventureNode root = null;

            // Assert
            Assert.Throws<TreeValidationException>(() => Guard.Against.NullRootNode(root, adventure));
        }

        [Fact]
        public void NullRootNodeGuard_RootIsNotNool_ShouldPass()
        {
            // Arrange
            var adventure = AdventureTestDataProvider.GetAdventureWithoutNodes(new Guid("7e67bfaf-4c85-4a44-afc5-4e46b6b085e1"));
            var root = new AdventureNode(new Guid("5c9b8041-dbbf-4665-9b0d-12a8e6890a0e"), Guid.Empty, "candidate");

            // Act
            var exception = Record.Exception(() => Guard.Against.NullRootNode(root, adventure));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void CyclicReferenceGuard_NodeVisitedBefore_ShouldThrowException()
        {
            // Arrange
            var nodeId = new Guid("7ef59d97-6437-4074-b63f-2d1a71241903");
            var parentId = new Guid("440d3048-67a2-4909-918a-4700031c3e69");
            var hashSet = new HashSet<Guid>();

            hashSet.Add(nodeId);
            hashSet.Add(parentId);

            // Assert
            var exception = Assert.Throws<TreeValidationException>(() => Guard.Against.CyclicReference(hashSet, nodeId, parentId));
            Assert.Equal(exception.GuardName, "CyclicReference");
        }

        [Fact]
        public void CyclicReferenceGuard_NodeIsNotVisited_ShouldReturnNodeId()
        {
            // Arrange
            var nodeId = new Guid("7ef59d97-6437-4074-b63f-2d1a71241903");
            var parentId = new Guid("440d3048-67a2-4909-918a-4700031c3e69");
            var hashSet = new HashSet<Guid>();

            hashSet.Add(parentId);

            // Act
            var result = Guard.Against.CyclicReference(hashSet, nodeId, parentId);

            // Assert
            Assert.Equal(result, nodeId);
        }

        [Fact]
        public void InvalidChildToParentReferenceGuard_ChildReferenceDifferentParent_ShouldThrowException()
        {
            // Arrange
            var adventureId = new Guid("b1b1372b-7330-4dba-8ae4-9df649f9c76a");
            var parentId = new Guid("213bedd9-92ce-4db7-a67c-0c3e4f3fc952");
            var childId = new Guid("b10494e5-7bd0-44cf-91ac-880c2ea58dbe");

            var parent = new AdventureNode(new Guid("213bedd9-92ce-4db7-a67c-0c3e4f3fc952"), adventureId, "Parent")
            {
                ParentId = null,
                LeftChildId = childId,
                RightChild = null
            };

            var child = new AdventureNode(childId, adventureId, "Child")
            {
                ParentId = new Guid("c30f0c5b-a94d-4192-945c-5d43ea5c7fc6"),
                LeftChildId = null,
                RightChild = null
            };

            // Assert
            var exception = Assert.Throws<TreeValidationException>(() => Guard.Against.InvalidChildToParentReference(child, parent.Id));
            Assert.Equal(exception.GuardName, "InvalidChildToParentReference");
        }

        [Fact]
        public void InvalidChildToParentReferenceGuard_CorrectReference_ShouldPass()
        {
            // Arrange
            var adventureId = new Guid("b1b1372b-7330-4dba-8ae4-9df649f9c76a");
            var parentId = new Guid("213bedd9-92ce-4db7-a67c-0c3e4f3fc952");
            var childId = new Guid("b10494e5-7bd0-44cf-91ac-880c2ea58dbe");

            var parent = new AdventureNode(parentId, adventureId, "Parent")
            {
                ParentId = null,
                LeftChildId = childId,
                RightChild = null
            };

            var child = new AdventureNode(childId, adventureId, "Child")
            {
                ParentId = parentId,
                LeftChildId = null,
                RightChild = null
            };

            // ACt
            var exception = Record.Exception(() => Guard.Against.InvalidChildToParentReference(child, parent.Id));

            // Assert
            Assert.Null(exception);
        }
    }
}