using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SmartCharging.Application.DTOs;

namespace SmartCharging.Tests.Integration;

public class GroupsApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public GroupsApiTests(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateGroup_ShouldReturn201()
    {
        var request = new { name = "Test Group", capacity = 100 };

        HttpResponseMessage response = await client.PostAsJsonAsync("/api/groups", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task GetGroup_WhenNotExists_ShouldReturn404()
    {
        HttpResponseMessage response = await client.GetAsync($"/api/groups/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateGroup_ThenGet_ShouldReturnSameGroup()
    {
        var request = new { name = "My Group", capacity = 200 };
        HttpResponseMessage createResponse = await client.PostAsJsonAsync("/api/groups", request);
        GroupDto? created = await createResponse.Content.ReadFromJsonAsync<GroupDto>();

        HttpResponseMessage getResponse = await client.GetAsync($"/api/groups/{created!.Id}");
        GroupDto? fetched = await getResponse.Content.ReadFromJsonAsync<GroupDto>();

        Assert.Equal("My Group", fetched!.Name);
        Assert.Equal(200, fetched.Capacity);
    }

    [Fact]
    public async Task CreateGroup_WithZeroCapacity_ShouldReturn400()
    {
        var request = new { name = "Bad Group", capacity = 0 };

        HttpResponseMessage response = await client.PostAsJsonAsync("/api/groups", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}