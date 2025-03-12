using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestInitProject.Application;
using TestInitProject.Application.Users.Commands.CreateUser;
using TestInitProject.Application.Users.Queries.GetUsersWithPagination;
using TestInitProject.Domain.Enums;
using TestInitProject.Infrastructure.Authentication;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase 
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/{id}")]
    public IActionResult GetUserById(Guid id)
    {
        return Ok(id);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
       var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpGet("")]
    [HasPermission(Permissions.ReadUser)]
    public async Task<IActionResult> GetUsers([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        return Ok(await _mediator.Send(new GetUsersWithPaginationQuery {
            PageNumber = pageNumber,
            PageSize = pageSize
        }));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginMember([FromBody] LoginCommand command)
    {
        string token = await _mediator.Send(command);

        return Ok(token);
    }
}
