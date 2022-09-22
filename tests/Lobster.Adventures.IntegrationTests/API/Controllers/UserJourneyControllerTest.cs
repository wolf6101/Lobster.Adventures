using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;

using Lobster.Adventures.Application.UserJourneys.Dtos;
using Lobster.Adventures.Domain.Entities;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Lobster.Adventures.IntegrationTests.API.Controllers
{
    public class UserJourneyControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UserJourneyControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                });
        }


        [Fact]
        public async void UserJourneyTest_Post_ValidPath_ShouldCreateUserJourney()
        {
            // Arrange
            var client = _factory.CreateClient();
            // seeded user and adventure
            var adventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e");
            var userId = new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e");

            // Path in seeded adventure
            var nodeId1 = new Guid("209005df-5897-4491-992e-c25cd9aca290");
            var nodeId21 = new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682");
            var nodeId31 = new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8");
            var nodeId41 = new Guid("096a086d-980b-44cb-bfa3-382d8f844ee2");

            var pathList = new List<Guid?>
            {
                null, nodeId1, nodeId21, nodeId31, nodeId41, null
            };

            var path = string.Join(',', pathList);

            var request = new CreateUserJourneyRequestDto()
            {
                AdventureId = adventureId,
                UserId = userId,
                Path = path
            };

            // Act
            var getResponse = await client.PostAsJsonAsync<CreateUserJourneyRequestDto>($"api/UserJourneys", request);

            var userJourneyDto = await getResponse.Content.ReadFromJsonAsync<UserJourneyDto>();

            // Assert
            Assert.Equal(HttpStatusCode.Created, getResponse.StatusCode);
            Assert.NotNull(userJourneyDto);
            Assert.Equal(request.AdventureId, userJourneyDto.AdventureId);
            Assert.Equal(request.UserId, userJourneyDto.UserId);
            Assert.Equal(request.Path, userJourneyDto.Path);
            Assert.Equal(UserJourneyStatusEnum.InProgress, userJourneyDto.Status);


            // Cleanup
            var deleteResponse = await client.DeleteAsync($"api/UserJourneys/{userJourneyDto.Id}");
            var deleteJourneyDto = deleteResponse.Content.ReadFromJsonAsync<UserJourneyDto>();
            Assert.NotNull(deleteJourneyDto);
        }

        [Fact]
        public async void UserJourneyControllerTest_PUT_ShouldCreateNewJourney()
        {
            // Arrange
            // seeded user and adventure
            var adventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e");
            var userId = new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e");

            // Path in seeded adventure
            var nodeId1 = new Guid("209005df-5897-4491-992e-c25cd9aca290");
            var nodeId21 = new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682");
            var nodeId31 = new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8");
            var nodeId41 = new Guid("096a086d-980b-44cb-bfa3-382d8f844ee2");

            var pathList = new List<Guid?>
            {
                null, nodeId1, nodeId21, nodeId31, nodeId41, null
            };

            var path = string.Join(',', pathList);
            var journeyId = new Guid("fd9779d3-00f5-46cd-93b5-4c87f727d971");

            var updateRequest = new UpdateUserJourneyRequestDto()
            {
                Id = journeyId,
                AdventureId = adventureId,
                UserId = userId,
                Path = path,
                Status = UserJourneyStatusEnum.Completed
            };

            var client = _factory.CreateClient();

            // Act
            var putResponse = await client.PutAsJsonAsync<UpdateUserJourneyRequestDto>("api/UserJourneys", updateRequest);

            var userJourneyDto = await putResponse.Content.ReadFromJsonAsync<UserJourneyDto>();

            var deleteResponse = await client.DeleteAsync($"api/UserJourneys/{userJourneyDto.Id}");
            var deleteUserJourneyDto = await deleteResponse.Content.ReadFromJsonAsync<UserJourneyDto>();

            // Assert
            Assert.Equal(HttpStatusCode.Created, putResponse.StatusCode); // should be created because used didn't exist in the system
            Assert.NotNull(userJourneyDto);
            Assert.Equal(userJourneyDto.Id, journeyId);
            Assert.Equal(userJourneyDto.AdventureId, updateRequest.AdventureId);
            Assert.Equal(userJourneyDto.UserId, updateRequest.UserId);
            Assert.Equal(userJourneyDto.Path, updateRequest.Path);
            Assert.Equal(userJourneyDto.Status, updateRequest.Status);

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.NotNull(deleteUserJourneyDto);
        }

        [Fact]
        public async void UserJourneyControllerTest_PUT_ShouldUpdateJourney()
        {
            // Arrange
            // seeded user and adventure
            var adventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e");
            var userId = new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e");

            // Path in seeded adventure
            var nodeId1 = new Guid("209005df-5897-4491-992e-c25cd9aca290");
            var nodeId21 = new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682");
            var nodeId31 = new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8");
            var nodeId41 = new Guid("096a086d-980b-44cb-bfa3-382d8f844ee2");

            var pathList = new List<Guid?>
            {
                null, nodeId1, nodeId21, nodeId31, nodeId41, null
            };

            var path = string.Join(',', pathList);
            var journeyId = new Guid("fd9779d3-00f5-46cd-93b5-4c87f727d971");

            var updateRequest = new UpdateUserJourneyRequestDto()
            {
                Id = journeyId,
                AdventureId = adventureId,
                UserId = userId,
                Path = path,
                Status = UserJourneyStatusEnum.Completed
            };

            var client = _factory.CreateClient();

            // Act
            // Creates
            var putResponse = await client.PutAsJsonAsync<UpdateUserJourneyRequestDto>("api/UserJourneys", updateRequest);
            // Updates
            var putResponse1 = await client.PutAsJsonAsync<UpdateUserJourneyRequestDto>("api/UserJourneys", updateRequest);

            var userJourneyDto = await putResponse.Content.ReadFromJsonAsync<UserJourneyDto>();
            var userJourneyDto1 = await putResponse1.Content.ReadFromJsonAsync<UserJourneyDto>();

            var deleteResponse = await client.DeleteAsync($"api/UserJourneys/{userJourneyDto.Id}");
            var deleteUserJourneyDto = await deleteResponse.Content.ReadFromJsonAsync<UserJourneyDto>();

            var deleteResponse1 = await client.DeleteAsync($"api/UserJourneys/{userJourneyDto1.Id}");
            // Assert
            Assert.Equal(HttpStatusCode.Created, putResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, putResponse1.StatusCode);
            Assert.NotNull(userJourneyDto1);
            Assert.Equal(userJourneyDto1.Id, journeyId);
            Assert.Equal(userJourneyDto1.AdventureId, updateRequest.AdventureId);
            Assert.Equal(userJourneyDto1.UserId, updateRequest.UserId);
            Assert.Equal(userJourneyDto1.Path, updateRequest.Path);
            Assert.Equal(userJourneyDto1.Status, updateRequest.Status);

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.NotNull(deleteUserJourneyDto);
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse1.StatusCode);
        }
    }
}