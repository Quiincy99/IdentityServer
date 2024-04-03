using Microsoft.EntityFrameworkCore;
using Moq;
using TestInitProject.Application.Common.Interfaces;
using TestInitProject.Application.Customers.Commands.CreateCustomer;
using TestInitProject.Domain.Customers;

namespace Application.UnitTests;


public class CreateCustomerCommandTests
{
    // private static CreateCustomerCommand Command = new();
    // private CreateCustomerCommandHandler _handler;
    // private Mock<DbSet<Customer>> _mockSet;
    // private Mock<IApplicationDbContext> _mockDbContext;

    // [SetUp]
    // public void SetUp()
    // {
    //     _mockSet = new Mock<DbSet<Customer>>();
    //     _mockDbContext = new Mock<IApplicationDbContext>();
    //     _handler = new CreateCustomerCommandHandler(_mockDbContext.Object);
    // }

    // [Test]
    // public async Task CreateCustomer_Should_Add_And_Save_Customer()
    // {
    //     // Arrange
    //     Command.Name = "test1";
    //     Command.Email = "test1@mailinator.com";

    //     _mockSet.Setup(m => m.Add(It.Is<Customer>(c => c.Name == Command.Name && c.Email == Command.Email)));
    //     _mockDbContext.Setup(m => m.Customers).Returns(_mockSet.Object);
    //     _mockDbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

    //     // Act
    //     int result = await _handler.Handle(Command, default);

    //     // Assert
    //     _mockSet.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once);
    //     _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    //     Assert.That(result, Is.EqualTo(1));
    // }
}
