using System;
using System.Collections.Generic;
using System.Linq;

using Lobster.Adventures.Domain.Entities;
using Lobster.Adventures.UnitTests.Domain.TestDataProviders;

using Xunit;

namespace Lobster.Adventures.UnitTests.Domain
{
    public class UserJourneyTest
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
        [Fact]
        public void UserJourney_SetPath_EmptyPath_ShouldThrowException()
        {
            // Arrange
            var journeyId = new Guid("c4ca48dd-5348-4732-ac61-85e591219b7b");
            // Act
            var journey = UserJourneyTestDataProvider.GetUserJourney(journeyId);
            // Assert
            Assert.Throws<ArgumentException>(() => journey.SetPath(""));
        }

        [Theory]
        [MemberData(nameof(NonExistingPaths))]

        public void UserJourney_SetPath_NonExistingPath_ShouldThrowException(string path)
        {
            // Arrange
            var journeyId = new Guid("c4ca48dd-5348-4732-ac61-85e591219b7b");

            // Act
            var journey = UserJourneyTestDataProvider.GetUserJourney(journeyId);

            // Assert
            Assert.Throws<TreeValidationException>(() => journey.SetPath(path));
        }

        [Fact]
        public void UserJourney_SetPath_LongerThanTreePath_ShouldThrowException()
        {
            // Arrange
            var journeyId = new Guid("c4ca48dd-5348-4732-ac61-85e591219b7b");
            var journey = UserJourneyTestDataProvider.GetUserJourney(journeyId);

            var pathList = new List<Guid?> {
                null,
                AdventureTestDataProvider.NODE_ID_1,
                AdventureTestDataProvider.NODE_ID_21,
                AdventureTestDataProvider.NODE_ID_31,
                AdventureTestDataProvider.NODE_ID_42,
                AdventureTestDataProvider.NODE_ID_42,
                AdventureTestDataProvider.NODE_ID_42,
                AdventureTestDataProvider.NODE_ID_42,
                null
            };

            var path = string.Join(',', pathList);

            // Assert
            Assert.Throws<TreeValidationException>(() => journey.SetPath(path));
        }

        [Theory]
        [MemberData(nameof(ExistingPaths))]
        public void UserJourney_SetPath_CorrectCompletePath_ShouldSet(string path)
        {
            // Arrange
            var journeyId = new Guid("c4ca48dd-5348-4732-ac61-85e591219b7b");
            var journey = UserJourneyTestDataProvider.GetUserJourney(journeyId);

            // Act
            var exception = Record.Exception(() => journey.SetPath(path));

            //Assert
            Assert.Null(exception);
        }

        [Theory]
        [MemberData(nameof(ExistingIncompletePaths))]
        public void UserJourney_SetPath_CorrectIncompletePath_ShouldSet(string path)
        {
            // Arrange
            var journeyId = new Guid("c4ca48dd-5348-4732-ac61-85e591219b7b");
            var journey = UserJourneyTestDataProvider.GetUserJourney(journeyId);

            // Act
            var exception = Record.Exception(() => journey.SetPath(path));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void UserJourney_SetPath_PathIsNotGuidString_ShouldThrowException()
        {
            // Arrange
            var journeyId = new Guid("c4ca48dd-5348-4732-ac61-85e591219b7b");
            var journey = UserJourneyTestDataProvider.GetUserJourney(journeyId);
            var path = ",1,2,3,4,";

            // Assert
            Assert.Throws<FormatException>(() => journey.SetPath(path));
        }

        [Fact]
        public void UserJourney_SetStatus_SetToCreatedAfterActioned_ShouldThrowException()
        {
            // Arrange
            var journeyId = new Guid("c4ca48dd-5348-4732-ac61-85e591219b7b");
            var journey = UserJourneyTestDataProvider.GetUserJourney(journeyId);

            var path = (string)ExistingIncompletePaths().ToList()[0][0];
            journey.SetPath(path);

            // Assert
            Assert.Throws<InvalidOperationException>(() => journey.SetStatus(UserJourneyStatusEnum.Created));
        }

        [Fact]
        public void UserJourney_SetStatus_SetToCompleted_ShouldPath()
        {
            // Arrange
            var journeyId = new Guid("c4ca48dd-5348-4732-ac61-85e591219b7b");
            var journey = UserJourneyTestDataProvider.GetUserJourney(journeyId);

            // Act
            var exception = Record.Exception(() => journey.SetStatus(UserJourneyStatusEnum.Completed));

            // Assert
            Assert.Null(exception);
        }

        public static IEnumerable<object[]> NonExistingPaths()
        {
            var pathList1 = new List<Guid?> {
                null,
                AdventureTestDataProvider.NODE_ID_1,
                AdventureTestDataProvider.NODE_ID_21,
                new Guid("a6101e06-41cc-4f5d-b6eb-c236834b4571"),
                AdventureTestDataProvider.NODE_ID_43,
                null
            };

            var pathList2 = new List<Guid?> {
                null,
                new Guid("a6101e06-41cc-4f5d-b6eb-c236834b4571"),
                AdventureTestDataProvider.NODE_ID_1,
                AdventureTestDataProvider.NODE_ID_21,
                AdventureTestDataProvider.NODE_ID_43,
                null
            };

            var pathList3 = new List<Guid?> {
                null,
                AdventureTestDataProvider.NODE_ID_1,
                AdventureTestDataProvider.NODE_ID_22,
                AdventureTestDataProvider.NODE_ID_31,
                AdventureTestDataProvider.NODE_ID_43,
                null
            };

            var pathList4 = new List<Guid?> {
                null,
                AdventureTestDataProvider.NODE_ID_1,
                AdventureTestDataProvider.NODE_ID_21,
                AdventureTestDataProvider.NODE_ID_31,
                AdventureTestDataProvider.NODE_ID_43,
                null
            };

            return new List<object[]>() {
                new object[] {string.Join(',', pathList1)},
                new object[] {string.Join(',', pathList2)},
                new object[] {string.Join(',', pathList3)},
                new object[] {string.Join(',', pathList4)},
            };
        }

        public static IEnumerable<object[]> ExistingPaths()
        {
            var pathList1 = new List<Guid?> {
                null,
                AdventureTestDataProvider.NODE_ID_1,
                AdventureTestDataProvider.NODE_ID_21,
                AdventureTestDataProvider.NODE_ID_31,
                AdventureTestDataProvider.NODE_ID_41,
                null
            };

            var pathList2 = new List<Guid?> {
                null,
                AdventureTestDataProvider.NODE_ID_1,
                AdventureTestDataProvider.NODE_ID_21,
                AdventureTestDataProvider.NODE_ID_31,
                AdventureTestDataProvider.NODE_ID_42,
                null
            };

            var pathList3 = new List<Guid?> {
                null,
                AdventureTestDataProvider.NODE_ID_1,
                AdventureTestDataProvider.NODE_ID_21,
                AdventureTestDataProvider.NODE_ID_32,
                AdventureTestDataProvider.NODE_ID_43,
                null
            };

            var pathList4 = new List<Guid?> {
                null,
                AdventureTestDataProvider.NODE_ID_1,
                AdventureTestDataProvider.NODE_ID_21,
                AdventureTestDataProvider.NODE_ID_32,
                AdventureTestDataProvider.NODE_ID_44,
                null
            };

            var pathList5 = new List<Guid?> {
                null,
                AdventureTestDataProvider.NODE_ID_1,
                AdventureTestDataProvider.NODE_ID_22,
                null
            };

            return new List<object[]>() {
                new object[] {string.Join(',', pathList1)},
                new object[] {string.Join(',', pathList2)},
                new object[] {string.Join(',', pathList3)},
                new object[] {string.Join(',', pathList4)},
                new object[] {string.Join(',', pathList5)},
            };
        }

        public static IEnumerable<object[]> ExistingIncompletePaths()
        {
            var pathList1 = new List<Guid?> {
                null,
                AdventureTestDataProvider.NODE_ID_1,
                null
            };

            var pathList2 = new List<Guid?> {
                null,
                AdventureTestDataProvider.NODE_ID_1,
                AdventureTestDataProvider.NODE_ID_21,
                null
            };

            var pathList3 = new List<Guid?> {
                null,
                AdventureTestDataProvider.NODE_ID_1,
                AdventureTestDataProvider.NODE_ID_21,
                AdventureTestDataProvider.NODE_ID_32,
                null
            };

            return new List<object[]>() {
                new object[] {string.Join(',', pathList1)},
                new object[] {string.Join(',', pathList2)},
                new object[] {string.Join(',', pathList3)},
            };
        }
    }
}