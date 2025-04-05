using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using TestInitProject.Application;
using TestInitProject.Application.Common.Exceptions;
using TestInitProject.Domain.Entities;

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
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("123456");
        User user = new User(Guid.NewGuid()).Create("test1@mailinator.com", "test", hashedPassword);
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

    // [Test]
    // public async Task Login_WithInvalidCredentials_ShouldThrowValidationException()
    // {
    //     // Arrange
    //     var user = new User { Id = Guid.NewGuid(), Email = "test1@mailinator.com", Password = "123456" };
    //     await UsingDbContext(db =>
    //     {
    //         db.Users.Add(user);
    //         db.SaveChanges();
    //     });

    //     var command = new LoginCommand { Email = "test1@mailinator.com", Password = "wrong" };

    //     // Act and Assert
    //     FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
    // }
}
