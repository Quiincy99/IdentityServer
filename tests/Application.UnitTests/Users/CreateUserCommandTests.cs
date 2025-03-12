using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using TestInitProject.Application;
using TestInitProject.Application.Users.Commands.CreateUser;
using TestInitProject.Domain.Entities;
using TestInitProject.Domain.Events;

namespace Application.UnitTests;


[TestFixture]
public class CreateUserCommandTests
{
    private CreateUserCommandHandler _handler;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<IUserRepository> _mockUserRepository;


    [SetUp]
    public void SetUp()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockUserRepository = new Mock<IUserRepository>();
        _handler = new CreateUserCommandHandler(_mockUnitOfWork.Object, _mockUserRepository.Object);
    }

    [Test]
    public async Task CreateUser_Should_Add_And_Save_User()
    {

        // Arrange
        CreateUserCommand command = new()
        {
            Name = "test1",
            Email = "test1@mailinator.com"
        };

        User entity = new User(Guid.NewGuid()).Create(command.Name, command.Email);

        _mockUserRepository.Setup(m => m.Add(entity));
        _mockUnitOfWork.Setup(m => m.SaveChangesAsync(default));

        // Act
        int result = await _handler.Handle(command, default);

        // Assert
        _mockUserRepository.Verify(m => m.Add(It.IsAny<User>()), Times.Once);
        _mockUnitOfWork.Verify(m => m.SaveChangesAsync(default), Times.Once);
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public async Task CreateUser_Should_Create_One_Domain_Event()
    {
        // Arrange
        CreateUserCommand command = new()
        {
            Name = "test1",
            Email = "test1@mailinator.com"
        };

        User entity = new User(Guid.NewGuid()).Create(command.Name, command.Email);

        _mockUserRepository.Setup(m => m.Add(entity));
        _mockUnitOfWork.Setup(m => m.SaveChangesAsync(default));

        // Act
        int result = await _handler.Handle(command, default);

        // Assert
        _mockUserRepository.Verify(m => m.Add(It.Is<User>(c => c.Name == "test1" && c.Email == "test1@mailinator.com")));
        _mockUserRepository.Verify(m => m.Add(It.Is<User>(c => c.DomainEvents.Count == 1 && c.DomainEvents.FirstOrDefault() is UserCreatedEvent)), Times.Once);
        Assert.That(result, Is.EqualTo(1));
    }
}
