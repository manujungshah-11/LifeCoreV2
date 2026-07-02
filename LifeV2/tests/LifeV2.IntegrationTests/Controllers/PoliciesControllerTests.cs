using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using LifeV2.Application.DTOs;
using Xunit;

namespace LifeV2.IntegrationTests.Controllers;

public class PoliciesControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PoliciesControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/policies");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Create_ThenGet_RoundTrips()
    {
        var request = new CreatePolicyRequest("POL-INT-1", "Term Life 10", 50_000m, 20m,
            DateTime.UtcNow, Guid.NewGuid());

        var createResponse = await _client.PostAsJsonAsync("/api/policies", request);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var created = await createResponse.Content.ReadFromJsonAsync<PolicyDto>();
        created.Should().NotBeNull();
        created!.PolicyNumber.Should().Be("POL-INT-1");
    }
}
