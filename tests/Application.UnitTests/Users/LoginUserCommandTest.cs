using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using IdentityServer.Application;
using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Domain.Entities;

namespace Application.UnitTests;

[TestFixture]
public class LoginUserCommandTests
{
    private LoginCommandHandler _handler;
    private Mock<IJwtProvider> _mockIJwtProvider;
    private Mock<IUserRepository> _mockUserRepository;



    [SetUp]
    public void SetUp()
    {
        _mockIJwtProvider = new Mock<IJwtProvider>();
        _mockUserRepository = new Mock<IUserRepository>();
        _handler = new LoginCommandHandler(_mockUserRepository.Object, _mockIJwtProvider.Object);
    }
    [Test]
    public async Task Login_WithValidCredentials_ShouldReturnToken()
    {
        // Arrange
        User user = new User(Guid.NewGuid()).Create("test1@mailinator.com", "test", "123456");
        LoginCommand command = new("test1@mailinator.com", "123456");

        _mockIJwtProvider.Setup(m => m.GenerateAsync(user)).Returns(Task.FromResult("token"));
        _mockUserRepository.Setup(m => m.GetUserByEmailAsync(command.Email)).Returns(Task.FromResult(user)!);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        _mockUserRepository.Verify(m => m.GetUserByEmailAsync(command.Email), Times.Once);
        _mockIJwtProvider.Verify(m => m.GenerateAsync(It.IsAny<User>()), Times.Once);
        Assert.That(result, Is.EqualTo("token"));
    }


    [Test]
    public void Login_WithInvalidCredentials_ShouldThrowValidationException()
    {
        // Arrange
        User user = new User(Guid.NewGuid()).Create("test1@mailinator.com", "test", "CorrectPassword");
        LoginCommand command = new("test1@mailinator.com", "IncorrectPassword");

        _mockUserRepository.Setup(m => m.GetUserByEmailAsync(command.Email)).Returns(Task.FromResult(user)!);

        // Act / Assert
        var ex = Assert.ThrowsAsync<UnauthorizationException>(async () =>
        {
            await _handler.Handle(command, default);
        });

        Assert.That(ex.Message, Is.EqualTo("Unauthorized access"));
    }

    [Test]
    public void Login_UnfoundCredentials_ShouldThrowValidationException()
    {
        // Arrange
        LoginCommand command = new("test1@mailinator.com", "Password");
        _mockUserRepository.Setup(m => m.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null!);

        // Act / Assert
        var ex = Assert.ThrowsAsync<UnauthorizationException>(async () =>
        {
            await _handler.Handle(command, default);
        });

        Assert.That(ex.Message, Is.EqualTo("Unauthorized access"));
    }
}
