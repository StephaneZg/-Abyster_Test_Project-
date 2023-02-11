
using System.Security.Claims;

namespace Abyster_Test_Project.Service.Contract;
 public interface ICurrentUserService{
    string? UserId { get; }
    ClaimsPrincipal? User { get; }
 }