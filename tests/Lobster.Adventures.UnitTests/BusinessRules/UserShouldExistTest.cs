using System;

using Lobster.Adventures.Domain.BusinessRuleValidators;
using Lobster.Adventures.Domain.Entities;
using Lobster.Adventures.Domain.Repositories;

using Moq;

using Xunit;

namespace Lobster.Adventures.UnitTests.BusinessRules
{
    public class UserRoleShouldExistTest
    {
        [Fact]
        public async void UserShouldExist_UserExists_ShouldReturnFalse()
        {
            // Arrange
            var id = new Guid("4ea82454-7296-4afb-9e05-09fc3e05fe38");
            var user = new User(id, "First Name", "Last Name", "email@email.com");

            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock.Setup(r => r.GetAsync(id)).ReturnsAsync(user);

            var rule = new UserShouldExist(id, repositoryMock.Object);

            // Act
            var result = await rule.IsBroken();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void UserShouldExist_UserDoesntExist_ShouldReturnTrue()
        {
            // Arrange
            var id = new Guid("4ea82454-7296-4afb-9e05-09fc3e05fe38");
            var user = new User(id, "First Name", "Last Name", "email@email.com");

            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock.Setup(r => r.GetAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            var rule = new UserShouldExist(id, repositoryMock.Object);

            // Act
            var result = await rule.IsBroken();

            // Assert
            Assert.True(result);
        }

    }
}