using System.Security.Claims;
using TestInitProject.Application.Common.Interfaces;
using TestInitProject.Application.Common.Interfaces.Auth;

namespace TestInitProject.Web;

public class CurrentUser : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? Id => Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid id) ? id : null;

    public bool IsAnonymous => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public string Email => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
}
