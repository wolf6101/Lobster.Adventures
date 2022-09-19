using System;
using System.Collections.Generic;
using System.Linq;

using Lobster.Adventures.Domain.Entities;
using Lobster.Adventures.UnitTests.Domain.TestDataProviders;

using Xunit;
using PrivateObject = Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject;

namespace Lobster.Adventures.UnitTests.Domain
{
    public class AdventureTest
    {
        [Fact]
        public void Adventure_SetNodes_NullInput_ShouldThrowException()
        {
            var adventureId = new Guid("81a04fda-e6db-463a-8c53-67edcef3100e");
            var adventure = AdventureTestDataProvider.GetAdventureWithoutNodes(adventureId);

            Assert.Throws<ArgumentNullException>(() => adventure.SetNodes(null));
            Assert.Throws<ArgumentException>(() => adventure.SetNodes(new List<AdventureNode>()));
        }

        [Fact]
        public void Adventure_SetNodes_Duplicates_ShouldThrowException()
        {
            var adventureId = new Guid("81a04fda-e6db-463a-8c53-67edcef3100e");
            var adventure = AdventureTestDataProvider.GetAdventureWithoutNodes(adventureId);
            var nodes = AdventureTestDataProvider.GetAdventureNodes(adventureId);
            var duplicate = new AdventureNode(nodes.First().Id, nodes.First().AdventureId, nodes.First().Name);
            nodes.Add(duplicate);

            Assert.Throws<ArgumentException>(() => adventure.SetNodes(nodes));//Fails on dictionary construction
        }

        [Fact]
        public void Adventure_SetNodes_TwoRoots_ShouldThrowException()
        {
            var adventureId = new Guid("81a04fda-e6db-463a-8c53-67edcef3100e");
            var adventure = AdventureTestDataProvider.GetAdventureWithoutNodes(adventureId);
            var nodes = AdventureTestDataProvider.GetAdventureNodes(adventureId);
            var root = new AdventureNode(new Guid("c8fff0e4-3c6d-400e-bb5e-25112004ea5a"), nodes.First().AdventureId, "Root")
            {
                ParentId = null
            };
            nodes.Add(root);

            var exception = Assert.Throws<TreeValidationException>(() => adventure.SetNodes(nodes));
            Assert.Equal(exception.GuardName, "MultipleRootNodes");
        }

        [Fact]
        public void Adventure_SetNodes_LeftChildDoesntExistInNodesList_ShouldThrowException()
        {
            var adventureId = new Guid("81a04fda-e6db-463a-8c53-67edcef3100e");
            var adventure = AdventureTestDataProvider.GetAdventureWithoutNodes(adventureId);
            var nodes = AdventureTestDataProvider.GetAdventureNodes(adventureId);

            var nodeWithWrongLeftRef = new AdventureNode(new Guid("be59a543-a1e9-49bd-8bf4-0c31478ecbd3"), adventureId, "Node")
            {
                ParentId = nodes.Last().Id,
                LeftChildId = new Guid("08a9f387-78e9-4b49-966f-5284868ca13c")
            };

            nodes.Add(nodeWithWrongLeftRef);

            var exception = Assert.Throws<TreeValidationException>(() => adventure.SetNodes(nodes));
            Assert.Equal(exception.GuardName, "InvalidNodeReference");
        }

        [Fact]
        public void Adventure_SetNodes_RightChildDoesntExistInNodesList_ShoualdThrowException()
        {
            var adventureId = new Guid("81a04fda-e6db-463a-8c53-67edcef3100e");
            var adventure = AdventureTestDataProvider.GetAdventureWithoutNodes(adventureId);
            var nodes = AdventureTestDataProvider.GetAdventureNodes(adventureId);

            var nodeWithWrongRightRef = new AdventureNode(new Guid("be59a543-a1e9-49bd-8bf4-0c31478ecbd3"), adventureId, "Node")
            {
                ParentId = nodes.Last().Id,
                LeftChildId = null,
                RightChildId = new Guid("08a9f387-78e9-4b49-966f-5284868ca13c"),
            };

            nodes.Add(nodeWithWrongRightRef);

            var exception = Assert.Throws<TreeValidationException>(() => adventure.SetNodes(nodes));
            Assert.Equal(exception.GuardName, "InvalidNodeReference");
        }

        [Fact]
        public void Adventure_SetNodes_ParentDoesntExistInNodesList_ShouldThrowException()
        {
            var adventureId = new Guid("81a04fda-e6db-463a-8c53-67edcef3100e");
            var adventure = AdventureTestDataProvider.GetAdventureWithoutNodes(adventureId);
            var nodes = AdventureTestDataProvider.GetAdventureNodes(adventureId);

            var nodeWithWrongParentRef = new AdventureNode(new Guid("be59a543-a1e9-49bd-8bf4-0c31478ecbd3"), adventureId, "Node")
            {
                ParentId = new Guid("08a9f387-78e9-4b49-966f-5284868ca13c"),
                LeftChildId = null,
                RightChildId = null,
            };

            nodes.Add(nodeWithWrongParentRef);

            var exception = Assert.Throws<TreeValidationException>(() => adventure.SetNodes(nodes));
            Assert.Equal(exception.GuardName, "InvalidNodeReference");
        }

        [Fact]
        public void Adventure_SetNodes_NoRoot_ShouldThrowException()
        {
            var adventureId = new Guid("81a04fda-e6db-463a-8c53-67edcef3100e");
            var adventure = AdventureTestDataProvider.GetAdventureWithoutNodes(adventureId);
            var nodes = AdventureTestDataProvider.GetAdventureNodes(adventureId);

            nodes[0].ParentId = nodes[nodes.Count - 1].Id;

            //Assert
            var exception = Assert.Throws<TreeValidationException>(() => adventure.SetNodes(nodes));
            Assert.Equal(exception.GuardName, "NullRootNode");
        }

        [Fact]
        public void Adventure_SetNodes_ChildReferenceDifferentParent_ShoudThrowException()
        {
            var adventureId = new Guid("81a04fda-e6db-463a-8c53-67edcef3100e");
            var adventure = AdventureTestDataProvider.GetAdventureWithoutNodes(adventureId);
            var nodes = AdventureTestDataProvider.GetAdventureNodes(adventureId);

            nodes[nodes.Count - 1].ParentId = nodes[0].Id;

            //Assert
            var exception = Assert.Throws<TreeValidationException>(() => adventure.SetNodes(nodes));
            Assert.Equal(exception.GuardName, "InvalidChildToParentReference");
        }

        [Fact]
        public void Adventure_SetNodes_JourneysActioned_ShoudPass()
        {
            var adventureId = new Guid("81a04fda-e6db-463a-8c53-67edcef3100e");
            var adventure = AdventureTestDataProvider.GetAdventureWithoutNodes(adventureId);
            var nodes = AdventureTestDataProvider.GetAdventureNodes(adventureId);

            var journeys = new List<UserJourney>() {
                new UserJourney(new Guid("5e874d95-b4ad-4172-9142-c2b760cfa18b"),
                    adventureId,
                    Guid.Empty)
            };

            var adventureWrapper = new PrivateObject(adventure);
            adventureWrapper.SetField("_journeys", journeys);

            //Assert
            Assert.Throws<InvalidOperationException>(() => adventure.SetNodes(nodes));
        }

        [Fact]
        public void Adventure_SetNodes_ValidInput_ShoudPass()
        {
            var adventureId = new Guid("81a04fda-e6db-463a-8c53-67edcef3100e");
            var adventure = AdventureTestDataProvider.GetAdventureWithoutNodes(adventureId);
            var nodes = AdventureTestDataProvider.GetAdventureNodes(adventureId);

            var exception = Record.Exception(() => adventure.SetNodes(nodes));

            //Assert
            Assert.Null(exception);
        }
    }
}