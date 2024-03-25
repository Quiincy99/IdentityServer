using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestInitProject.Application;
using TestInitProject.Application.Customers.Commands.CreateCustomer;
using TestInitProject.Application.Customers.Queries.GetCustomersWithPagination;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase 
{
    private readonly IMediator _mediator;
    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/{id}")]
    public IActionResult GetCustomerById(Guid id)
    {
        return Ok(id);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
       var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetCustomers([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        return Ok(await _mediator.Send(new GetCustomersWithPaginationQuery {
            PageNumber = pageNumber,
            PageSize = pageSize
        }));
    }
}
