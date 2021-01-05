using System.Collections.Generic;
using System.Security.Claims;

namespace Vanir.Utilities.Interfaces
{
    public interface ITokenProvider
    {
        string Get(string username, List<Claim> claims = null);
    }
}