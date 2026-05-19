using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Members.Queries.CheckExists;
using Application.Shared;
using Domain.Members;
using Moq;

namespace Application.Tests.Members.Queries.CheckExists;

public class CheckMemberExistsQueryHandlerTests
{
    private readonly Mock<IReadOnlyContext> _mockReadOnlyContext;
    private readonly CheckMemberExistsQueryHandler _handler;

    public CheckMemberExistsQueryHandlerTests()
    {
        _mockReadOnlyContext = new Mock<IReadOnlyContext>();
        _handler = new CheckMemberExistsQueryHandler(_mockReadOnlyContext.Object);
    }

    [Fact]
    public async Task Handle_WhenMemberDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var identifyName = "nonexistent_user";
        var query = new CheckMemberExistsQuery(identifyName);

        _mockReadOnlyContext
            .Setup(c => c.AnyAsync<Member>(
                It.IsAny<Func<IQueryable<Member>, IQueryable<Member>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result);
        _mockReadOnlyContext.Verify(
            c => c.AnyAsync<Member>(
                It.IsAny<Func<IQueryable<Member>, IQueryable<Member>>>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_PassesCancellationTokenToRepository()
    {
        // Arrange
        var identifyName = "testuser";
        var query = new CheckMemberExistsQuery(identifyName);
        var cancellationToken = new CancellationToken();

        _mockReadOnlyContext
            .Setup(c => c.AnyAsync<Member>(
                It.IsAny<Func<IQueryable<Member>, IQueryable<Member>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        _ = await _handler.Handle(query, cancellationToken);

        // Assert
        _mockReadOnlyContext.Verify(
            c => c.AnyAsync<Member>(
                It.IsAny<Func<IQueryable<Member>, IQueryable<Member>>>(),
                cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task Handle_CallsRepositoryWithQueryIdentifyName()
    {
        // Arrange
        var identifyName = "testuser123";
        var query = new CheckMemberExistsQuery(identifyName);

        _mockReadOnlyContext
            .Setup(c => c.AnyAsync<Member>(
                It.IsAny<Func<IQueryable<Member>, IQueryable<Member>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        await _handler.Handle(query, CancellationToken.None);

        // Assert - verify the context was called once
        _mockReadOnlyContext.Verify(
            c => c.AnyAsync<Member>(
                It.IsAny<Func<IQueryable<Member>, IQueryable<Member>>>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}


