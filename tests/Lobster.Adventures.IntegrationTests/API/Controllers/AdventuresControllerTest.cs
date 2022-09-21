using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;

using Lobster.Adventures.Application.Adventures.Dtos;

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
        public async void AdventuresControllerTest_GetAll_ShouldReturn1Adventure()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var getResponse = await client.GetAsync("api/Adventures");
            var adventureDtos = await getResponse.Content.ReadFromJsonAsync<IList<AdventureDto>>();
            var adventureDto = adventureDtos.FirstOrDefault();

            // Assert
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.NotNull(adventureDtos);
            Assert.Equal(1, adventureDtos.Count);
            Assert.Equal("Doughnut adventure", adventureDto.Name);
            Assert.Equal(9, adventureDto.NumberOfNodes);
            Assert.Equal("Adventure", adventureDto.Description);
            Assert.Equal(new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), adventureDto.Id);
            Assert.Equal(new Guid("209005df-5897-4491-992e-c25cd9aca290"), adventureDto.RootNodeId);
        }

        [Fact]
        public async void AdventuresControllerTest_GetWithoutNodes_ShouldReturnAdventure()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var id = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e");
            var withNodes = false;
            var getResponse = await client.GetAsync($"api/Adventures/{id}?withNodes={withNodes}");
            var adventureDto = await getResponse.Content.ReadFromJsonAsync<AdventureDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.NotNull(adventureDto);
            Assert.Equal("Doughnut adventure", adventureDto.Name);
            Assert.Equal(9, adventureDto.NumberOfNodes);
            Assert.Equal("Adventure", adventureDto.Description);
            Assert.Equal(new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), adventureDto.Id);
            Assert.Equal(new Guid("209005df-5897-4491-992e-c25cd9aca290"), adventureDto.RootNodeId);
            Assert.Empty(adventureDto.Nodes);

        }

        [Fact]
        public async void AdventuresControllerTest_GetWithNodes_ShouldReturnAdventure()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var id = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e");
            var withNodes = true;
            var getResponse = await client.GetAsync($"api/Adventures/{id}?withNodes={withNodes}");
            var adventureDto = await getResponse.Content.ReadFromJsonAsync<AdventureDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.NotNull(adventureDto);
            Assert.Equal("Doughnut adventure", adventureDto.Name);
            Assert.Equal(9, adventureDto.NumberOfNodes);
            Assert.Equal("Adventure", adventureDto.Description);
            Assert.Equal(new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), adventureDto.Id);
            Assert.Equal(new Guid("209005df-5897-4491-992e-c25cd9aca290"), adventureDto.RootNodeId);
            Assert.Equal(9, adventureDto.Nodes.Count);
        }

        [Fact]
        public async void AdventuresControllerTest_Post_ShouldCreateAdventure()
        {
            // Arrange
            var client = _factory.CreateClient();

            var node1 = new Guid("b1077235-7e36-4aee-872b-8913e29e4ebe");
            var node21 = new Guid("ac5e3a22-25e7-42aa-907a-917636987192");
            var node22 = new Guid("6cf2c9f6-31e8-4160-a4b4-0921556b7c82");
            var node31 = new Guid("9aa308a3-f2c5-48b2-b25d-3603410c09fb");

            var nodes = new List<CreateAdventureNodeRequestDto> {
                new CreateAdventureNodeRequestDto() {
                    Id = node1,
                    ParentId = null,
                    Name = "I am root",
                    LeftChildId = node21,
                    RightChildId = node22,
                },
                new CreateAdventureNodeRequestDto() {
                    Id = node21,
                    ParentId = node1,
                    Name = "I am L2 left child",
                    LeftChildId = node31,
                    RightChildId = null,
                },
                new CreateAdventureNodeRequestDto() {
                    Id = node22,
                    ParentId = node1,
                    Name = "I am L2 right child",
                    LeftChildId = null,
                    RightChildId = null,
                },
                new CreateAdventureNodeRequestDto() {
                    Id = node31,
                    ParentId = node21,
                    Name = "I am L3 left chile",
                    LeftChildId = null,
                    RightChildId = null,
                }
            };

            var request = new CreateAdventureRequestDto
            {
                Name = "New Adventure",
                Description = "New adventure description"
            };

            request.Nodes = nodes;

            // Act
            var getResponse = await client.PostAsJsonAsync<CreateAdventureRequestDto>($"api/Adventures", request);

            var adventureDto = await getResponse.Content.ReadFromJsonAsync<AdventureDto>();

            // Assert
            Assert.Equal(HttpStatusCode.Created, getResponse.StatusCode);
            Assert.NotNull(adventureDto);
            Assert.Equal(request.Name, adventureDto.Name);
            Assert.Equal(4, adventureDto.NumberOfNodes);
            Assert.Equal(request.Description, adventureDto.Description);
            Assert.Equal(node1, adventureDto.RootNodeId);

            // Cleanup
            var deleteResponse = await client.DeleteAsync($"api/Adventures/{adventureDto.Id}");
            var deleteAdventureDto = deleteResponse.Content.ReadFromJsonAsync<AdventureDto>();
            Assert.NotNull(deleteAdventureDto);
        }

        [Fact]
        public async void AdventuresControllerTest_Post_InvalidTree_ShouldReturnConflict()
        {
            // Arrange
            var client = _factory.CreateClient();

            var node1 = new Guid("b1077235-7e36-4aee-872b-8913e29e4ebe");
            var node21 = new Guid("ac5e3a22-25e7-42aa-907a-917636987192");
            var node22 = new Guid("6cf2c9f6-31e8-4160-a4b4-0921556b7c82");
            var node31 = new Guid("9aa308a3-f2c5-48b2-b25d-3603410c09fb");

            var nodes = new List<CreateAdventureNodeRequestDto> {
                new CreateAdventureNodeRequestDto() {
                    Id = node1,
                    ParentId = null,
                    Name = "I am root",
                    LeftChildId = node21,
                    RightChildId = node22,
                },
                new CreateAdventureNodeRequestDto() {
                    Id = node21,
                    ParentId = node1,
                    Name = "I am L2 left child",
                    LeftChildId = node31,
                    RightChildId = null,
                },
                new CreateAdventureNodeRequestDto() {
                    Id = node22,
                    ParentId = node1,
                    Name = "I am L2 right child",
                    LeftChildId = null,
                    RightChildId = null,
                },
                new CreateAdventureNodeRequestDto() {
                    Id = node31,
                    ParentId = node1,
                    Name = "I am L2 extra child",
                    LeftChildId = null,
                    RightChildId = null,
                }
            };

            var request = new CreateAdventureRequestDto
            {
                Name = "New Adventure",
                Description = "New adventure description"
            };

            request.Nodes = nodes;

            // Act
            var getResponse = await client.PostAsJsonAsync<CreateAdventureRequestDto>($"api/Adventures", request);

            var errorMessage = await getResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, getResponse.StatusCode);
            Assert.StartsWith("Broken child to parent reference", errorMessage);
        }
    }
}