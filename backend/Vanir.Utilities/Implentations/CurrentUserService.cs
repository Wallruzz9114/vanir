using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Vanir.Utilities.Interfaces;

namespace Vanir.Utilities.Implentations
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public ClaimsPrincipal GetClaimsPrincipal() => _httpContextAccessor.HttpContext.User;
    }
}