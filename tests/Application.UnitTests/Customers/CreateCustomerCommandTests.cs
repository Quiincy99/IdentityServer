using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using TestInitProject.Application;
using TestInitProject.Application.Customers.Commands.CreateCustomer;
using TestInitProject.Domain.Entities;
using TestInitProject.Domain.Events;

namespace Application.UnitTests;


[TestFixture]
public class CreateCustomerCommandTests
{
    private CreateCustomerCommandHandler _handler;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<ICustomerRepository> _mockCustomerRepository;


    [SetUp]
    public void SetUp()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        _handler = new CreateCustomerCommandHandler(_mockUnitOfWork.Object, _mockCustomerRepository.Object);
    }

    [Test]
    public async Task CreateCustomer_Should_Add_And_Save_Customer()
    {

        // Arrange
        CreateCustomerCommand command = new()
        {
            Name = "test1",
            Email = "test1@mailinator.com"
        };
 
        Customer entity = new Customer(Guid.NewGuid()).Create(command.Name, command.Email);

        _mockCustomerRepository.Setup(m => m.Add(entity));
        _mockUnitOfWork.Setup(m => m.SaveChangesAsync(default));   

        // Act
        int result = await _handler.Handle(command, default);

        // Assert
        _mockCustomerRepository.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once);
        _mockUnitOfWork.Verify(m => m.SaveChangesAsync(default), Times.Once);
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public async Task CreateCustomer_Should_Create_One_Domain_Event()
    {
        // Arrange
        CreateCustomerCommand command = new()
        {
            Name = "test1",
            Email = "test1@mailinator.com"
        };
 
        Customer entity = new Customer(Guid.NewGuid()).Create(command.Name, command.Email);

        _mockCustomerRepository.Setup(m => m.Add(entity));
        _mockUnitOfWork.Setup(m => m.SaveChangesAsync(default));   

        // Act
        int result = await _handler.Handle(command, default);

        // Assert
        _mockCustomerRepository.Verify(m => m.Add(It.Is<Customer>(c => c.Name == "test1" && c.Email == "test1@mailinator.com")));
        _mockCustomerRepository.Verify(m => m.Add(It.Is<Customer>(c => c.DomainEvents.Count == 1 && c.DomainEvents.FirstOrDefault() is CustomerCreatedEvent)), Times.Once);
        Assert.That(result, Is.EqualTo(1));
    }
}
