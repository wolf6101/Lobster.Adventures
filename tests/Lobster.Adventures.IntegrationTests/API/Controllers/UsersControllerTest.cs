using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;

using Lobster.Adventures.Application.Users.Dtos;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Lobster.Adventures.IntegrationTests.API.Controllers
{
    public class UsersControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UsersControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                });
        }

        [Fact]
        public async void AdventuresControllerTest_GetAll_ShouldReturn1SeededUser()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var getResponse = await client.GetAsync("api/Users");
            var usersDtos = await getResponse.Content.ReadFromJsonAsync<IList<UserDto>>();
            var userDto = usersDtos.First();

            // Assert
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.NotNull(usersDtos);

            Assert.Equal(new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e"), userDto.Id);
            Assert.Equal(1, usersDtos.Count);
            Assert.Equal("John", userDto.FirstName);
            Assert.Equal("Smith", userDto.LastName);
            Assert.Equal("johnsmith@email.com", userDto.Email);
        }
    }
}