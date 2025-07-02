using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Queries.GetItem;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;

namespace WebApi.Tests.Controllers
{
    public class ItemsControllerTests 
        : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ItemsControllerTests(WebApplicationFactory<Program> factory)
        {
            // Koristimo in-memory test server
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_WhenNoItems_ReturnsEmptyList()
        {
            var response = await _client.GetAsync("/api/items");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var items = await response.Content.ReadFromJsonAsync<IEnumerable<ItemDto>>();
            items.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public async Task Post_And_GetById_WorksEndToEnd()
        {
            // Arrange
            var command = new { Name = "IntegrationTest", Price = 5.5m };

            // Act: kreiraj stavku
            var postResponse = await _client.PostAsJsonAsync("/api/items", command);
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdId = await postResponse.Content.ReadFromJsonAsync<int>();
            createdId.Should().BeGreaterThan(0);

            // Act: dohvati istu stavku
            var getResponse = await _client.GetAsync($"/api/items/{createdId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dto = await getResponse.Content.ReadFromJsonAsync<ItemDto>();
            dto.Should().NotBeNull();
            dto.Id.Should().Be(createdId);
            dto.Name.Should().Be(command.Name);
            dto.Price.Should().Be(command.Price);
        }

        [Fact]
        public async Task GetById_WhenNotExists_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/items/9999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}