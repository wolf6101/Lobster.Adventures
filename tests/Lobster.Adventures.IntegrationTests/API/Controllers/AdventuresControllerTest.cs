using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Lobster.Adventures.IntegrationTests.API.Controllers
{
    public class AdventuresControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public AdventuresControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                });
        }

        [Fact]
        public async void UserControllerTest_POST_ShouldCreateNewUser()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var postResponse = await client.GetAsync("api/Adventures");

            // Assert
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        }
    }
}