
using System.Security.Claims;
using Abyster_Test_Project.Service.Contract;

namespace Abyster_Test_Project.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
    public string? FullName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
}