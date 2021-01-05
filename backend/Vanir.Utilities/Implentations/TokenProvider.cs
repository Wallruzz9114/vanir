using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Vanir.Utilities.Interfaces;

namespace Vanir.Utilities.Implentations
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;
        public TokenProvider(IConfiguration configuration) => _configuration = configuration;

        public string Get(string uniqueName, List<Claim> claims = null)
        {
            var now = DateTime.UtcNow;
            var nowDateTimeOffset = new DateTimeOffset(now);

            var newClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, uniqueName),
                new Claim(JwtRegisteredClaimNames.Sub, uniqueName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, nowDateTimeOffset.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            };

            if (claims != null) newClaims.AddRange(claims);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtIssuer"],
                audience: _configuration["Authentication:JwtAudience"],
                claims: newClaims,
                notBefore: now,
                expires: now.AddMinutes(Convert.ToInt16(_configuration["Authentication:MinutesToExpiration"])),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:JwtKey"])), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}