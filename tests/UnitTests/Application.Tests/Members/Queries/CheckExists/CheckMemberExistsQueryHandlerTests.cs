using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Members.Queries.CheckExists;
using Domain.Members;
using Moq;

namespace Application.Tests.Members.Queries.CheckExists;

public class CheckMemberExistsQueryHandlerTests
{
    private readonly Mock<IMemberRepository> _mockMemberRepository;
    private readonly CheckMemberExistsQueryHandler _handler;

    public CheckMemberExistsQueryHandlerTests()
    {
        _mockMemberRepository = new Mock<IMemberRepository>();
        _handler = new CheckMemberExistsQueryHandler(_mockMemberRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenMemberDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var identifyName = "nonexistent_user";
        var query = new CheckMemberExistsQuery(identifyName);

        _mockMemberRepository
            .Setup(r => r.GetByIdentifyNameAsync(identifyName, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Member?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result);
        _mockMemberRepository.Verify(
            r => r.GetByIdentifyNameAsync(identifyName, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_PassesCancellationTokenToRepository()
    {
        // Arrange
        var identifyName = "testuser";
        var query = new CheckMemberExistsQuery(identifyName);
        var cancellationToken = new CancellationToken();

        _mockMemberRepository
            .Setup(r => r.GetByIdentifyNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Member?)null);

        // Act
        _ = await _handler.Handle(query, cancellationToken);

        // Assert
        _mockMemberRepository.Verify(
            r => r.GetByIdentifyNameAsync(identifyName, cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task Handle_CallsRepositoryWithQueryIdentifyName()
    {
        // Arrange
        var identifyName = "testuser123";
        var query = new CheckMemberExistsQuery(identifyName);

        _mockMemberRepository
            .Setup(r => r.GetByIdentifyNameAsync(identifyName, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Member?)null);

        // Act
        await _handler.Handle(query, CancellationToken.None);

        // Assert - verify the repository was called with the correct identify name
        _mockMemberRepository.Verify(
            r => r.GetByIdentifyNameAsync(identifyName, It.IsAny<CancellationToken>()),
            Times.Once);
    }
}


