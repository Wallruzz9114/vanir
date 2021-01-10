using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vanir.Infrastructure.Features.Identity;
using Vanir.Utilities.Wrappers;

namespace Vanir.API.Controllers
{
    public class IdentityController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AuthenticationResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthenticationResponse>> Login(Login.Query loginQuery) =>
            await Mediator.Send(loginQuery);
    }
}