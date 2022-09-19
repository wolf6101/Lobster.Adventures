using System;
using System.Collections.Generic;

using Lobster.Adventures.Domain.Entities;

namespace Lobster.Adventures.UnitTests.Domain.TestDataProviders
{
    public static class AdventureTestDataProvider
    {
        /*
        Tree representation:
                     1
                   /   \
                  2.1  2.2
                /     \
              3.1     3.2
             /  \     /   \
           4.1  4.2 4.3  4.4
        */
        public static Guid NODE_ID_1 => new Guid("209005df-5897-4491-992e-c25cd9aca290");
        public static Guid NODE_ID_21 => new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682");
        public static Guid NODE_ID_22 => new Guid("f287a87e-148c-4ffe-aed7-37bb6baefbb8");
        public static Guid NODE_ID_31 => new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8");
        public static Guid NODE_ID_32 => new Guid("130454c8-4a63-40b3-b400-b2b13dc34809");
        public static Guid NODE_ID_41 => new Guid("096a086d-980b-44cb-bfa3-382d8f844ee2");
        public static Guid NODE_ID_42 => new Guid("95069404-d73c-4ba9-8a1e-5f76bb51e790");
        public static Guid NODE_ID_43 => new Guid("4ae2afff-92e8-4ac3-b934-3d07be023f3d");
        public static Guid NODE_ID_44 => new Guid("2f6a6663-90f8-4313-8441-dda39df5d677");

        public static Adventure GetAdventureWithoutNodes(Guid adventureId)
        {
            var adventure = new Adventure(adventureId, "Doughnut adventure", "Adventure");

            return adventure;
        }

        public static Adventure GetAdventure(Guid adventureId)
        {
            var adventure = new Adventure(adventureId, "Doughnut adventure", "Adventure");
            adventure.SetNodes(GetAdventureNodes(adventureId));

            return adventure;
        }

        public static IList<AdventureNode> GetAdventureNodes(Guid adventureId)
        {
            var nodes = new List<AdventureNode> {
                new AdventureNode(NODE_ID_1, adventureId, "Do I want Doughnut?")
                {
                    ParentId = null,
                    LeftChildId = NODE_ID_21,
                    RightChildId = NODE_ID_22
                },
                new AdventureNode(NODE_ID_21, adventureId, "Do I deserve it?")
                {
                    ParentId = NODE_ID_1,
                    LeftChildId = NODE_ID_31,
                    RightChildId = NODE_ID_32
                },
                new AdventureNode(NODE_ID_22, adventureId, "Maybe you want an apple?") {
                    ParentId = NODE_ID_1,
                    LeftChildId = null,
                    RightChildId = null
                },
                new AdventureNode(NODE_ID_31, adventureId, "Are you sure?") {
                    ParentId = NODE_ID_21,
                    LeftChildId = NODE_ID_41,
                    RightChildId = NODE_ID_42
                },
                new AdventureNode(NODE_ID_32, adventureId, "Is it a good doughnut?") {
                    ParentId = NODE_ID_21,
                    LeftChildId = NODE_ID_43,
                    RightChildId = NODE_ID_44
                },
                new AdventureNode(NODE_ID_41, adventureId, "Get it!") {
                    ParentId = NODE_ID_31,
                    LeftChildId = null,
                    RightChildId = null
                },
                new AdventureNode(NODE_ID_42, adventureId, "Do jumping jacks first!") {
                    ParentId = NODE_ID_31,
                    LeftChildId = null,
                    RightChildId = null
                },
                new AdventureNode(NODE_ID_43, adventureId, "Grab it now!") {
                    ParentId = NODE_ID_32,
                    LeftChildId = null,
                    RightChildId = null
                },
                new AdventureNode(NODE_ID_44, adventureId, "Wait for a better one!") {
                    ParentId = NODE_ID_32,
                    LeftChildId = null,
                    RightChildId = null
                }
            };

            return nodes;
        }
    }
}