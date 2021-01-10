using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vanir.Utilities.Interfaces;
using Vanir.Utilities.Models;
using Vanir.Utilities.Wrappers;

namespace Vanir.Infrastructure.Features.Identity
{
    public class Login
    {
        public class Query : IRequest<AuthenticationResponse>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, AuthenticationResponse>
        {
            private readonly IAppDatabaseContext _appDatabaseContext;
            private readonly ITokenProvider _tokenProvider;
            private readonly IPasswordHasher _passwordHasher;

            public Handler(IAppDatabaseContext appDatabaseContext, ITokenProvider tokenProvider, IPasswordHasher passwordHasher)
            {
                _passwordHasher = passwordHasher;
                _tokenProvider = tokenProvider;
                _appDatabaseContext = appDatabaseContext;
            }

            public async Task<AuthenticationResponse> Handle(Query query, CancellationToken cancellationToken)
            {
                var user = await _appDatabaseContext.Set<User>()
                    .SingleOrDefaultAsync(x => x.Username.ToLower() == query.Username.ToLower(), cancellationToken);

                if (user == null) throw new Exception($"Could not find user: { query.Username }");

                var hashedPassword = _passwordHasher.HashPassword(user.Salt, query.Password);

                if (!ValidUser(user, hashedPassword))
                    throw new Exception($"Could not validate user: { query.Username }");

                var schemaType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
                var claims = user?.Roles.Select(x => new Claim(schemaType, x.Name)).ToList();
                var token = _tokenProvider.Get(query.Username, claims);

                return new AuthenticationResponse
                {
                    Token = token,
                    Username = user.Username
                };
            }

            public static bool ValidUser(User user, string transformedPassword)
            {
                if (user == null || transformedPassword == null) return false;
                return user.Password == transformedPassword;
            }
        }
    }
}