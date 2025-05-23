using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using IdentityServer.Application;
using IdentityServer.Application.Common.Interfaces.Auth;
using IdentityServer.Application.Users.Commands.CreateUser;
using IdentityServer.Application.Users.Queries.GetUsersWithPagination;
using IdentityServer.Domain.Enums;
using IdentityServer.Infrastructure.Authentication;

namespace IdentityServer.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContext _userContext;
    public UserController(IMediator mediator, IUserContext userContext)
    {
        _mediator = mediator;
        _userContext = userContext;
    }

    [HttpGet("{id}")]
    [ProducesResponseType<Guid>(200)]
    [Authorize]
    public IActionResult GetUserById(Guid id)
    {
        Console.WriteLine("Email: " + _userContext.Email);
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
        return Ok(await _mediator.Send(new GetUsersWithPaginationQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        }));
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginMember([FromBody] LoginCommand command)
    {
        string token = await _mediator.Send(command);

        if (token.IsNullOrEmpty())
        {
            return Unauthorized("Unauthorized request");
        }

        return Ok(token);
    }
}
