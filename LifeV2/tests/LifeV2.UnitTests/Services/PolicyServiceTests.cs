using FluentAssertions;
using LifeV2.Application.Common;
using LifeV2.Application.DTOs;
using LifeV2.Application.Services;
using LifeV2.Domain.Entities;
using LifeV2.Domain.Enums;
using LifeV2.Domain.Interfaces;
using Moq;
using Xunit;

namespace LifeV2.UnitTests.Services;

public class PolicyServiceTests
{
    private readonly Mock<IPolicyRepository> _repository = new();
    private readonly PolicyService _sut;

    public PolicyServiceTests()
    {
        _sut = new PolicyService(_repository.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WhenPolicyExists_ReturnsDto()
    {
        // Arrange
        var policy = new Policy
        {
            PolicyNumber = "POL-001",
            ProductName = "Term Life 20",
            CoverageAmount = 100_000m,
            MonthlyPremium = 35m,
            Status = PolicyStatus.Active
        };
        _repository.Setup(r => r.GetByIdAsync(policy.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(policy);

        // Act
        var result = await _sut.GetByIdAsync(policy.Id);

        // Assert
        result.PolicyNumber.Should().Be("POL-001");
        result.Status.Should().Be(PolicyStatus.Active);
    }

    [Fact]
    public async Task GetByIdAsync_WhenPolicyMissing_ThrowsNotFound()
    {
        // Arrange
        _repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Policy?)null);

        // Act
        var act = () => _sut.GetByIdAsync(Guid.NewGuid());

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task CreateAsync_PersistsAndReturnsDto()
    {
        // Arrange
        var request = new CreatePolicyRequest("POL-777", "Whole Life", 250_000m, 90m,
            DateTime.UtcNow, Guid.NewGuid());
        _repository.Setup(r => r.AddAsync(It.IsAny<Policy>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Policy p, CancellationToken _) => p);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        result.PolicyNumber.Should().Be("POL-777");
        _repository.Verify(r => r.AddAsync(It.IsAny<Policy>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
