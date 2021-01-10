using System.Security.Claims;

namespace Vanir.Utilities.Interfaces
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal GetClaimsPrincipal();
    }
}