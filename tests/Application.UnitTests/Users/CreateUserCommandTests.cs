using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using TestInitProject.Application;
using TestInitProject.Application.Common.Behaviours;
using TestInitProject.Application.Common.Exceptions;
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
            Email = "test1@mailinator.com",
            Password = "123456"
        };

        User entity = new User(Guid.NewGuid()).Create(command.Name, command.Email, command.Password);

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
            Email = "test1@mailinator.com",
            Password = "123456"
        };

        User entity = new User(Guid.NewGuid()).Create(command.Name, command.Email, command.Password);

        _mockUserRepository.Setup(m => m.Add(entity));
        _mockUnitOfWork.Setup(m => m.SaveChangesAsync(default));

        // Act
        int result = await _handler.Handle(command, default);

        // Assert
        _mockUserRepository.Verify(m => m.Add(It.Is<User>(c => c.Name == "test1" && c.Email == "test1@mailinator.com")));
        _mockUserRepository.Verify(m => m.Add(It.Is<User>(c => c.DomainEvents.Count == 1 && c.DomainEvents.FirstOrDefault() is UserCreatedEvent)), Times.Once);
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void ValidationBehaviour_InvalidRequest_ShouldThrowValidationException()
    {
        // Arrange
        CreateUserCommand command = new()
        {
            Email = "invalidemail",
        };

        var validatorMock = new Mock<IValidator<CreateUserCommand>>();
        validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<CreateUserCommand>>(), default))
            .ReturnsAsync(new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Email", "Invalid email format.")
            }));

        var validators = new List<IValidator<CreateUserCommand>> { validatorMock.Object };

        var behavior = new ValidationBehavior<CreateUserCommand, string>(validators);

        RequestHandlerDelegate<string> next = () => { return Task.FromResult(string.Empty); };

        // Act & Assert
        Assert.ThrowsAsync<TestInitProject.Application.Common.Exceptions.ValidationException>(() => behavior.Handle(command, next, default));
    }
}
