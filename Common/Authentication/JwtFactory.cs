using Common.Constants;
using Common.Entities;
using Common.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Common.Authentication
{
    public class JwtFactory
    {
        /// <summary>
        /// Jwt Options 
        /// </summary>
        private readonly JwtIssuerOptions _jwtOptions;

        /// <summary>
        /// JwtFactory 
        /// </summary>
        /// <param name="jwtOptions">The Jwt Options</param>
        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        /// <summary>
        /// GenerateEncodedToken
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="identity">identity</param>
        /// <returns></returns>
        public async Task<string> GenerateEncodedToken(AppUser user, List<string> roles)
        {
            var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Id),
        new Claim(ClaimTypes.GivenName, user.FirstName),
        new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
      };

            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            // Create the JWT security token and encode it.
            return new JwtSecurityTokenHandler().WriteToken(
              new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials));
        }

        /// <summary>
        ///   GenerateClaimsIdentity
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(KeyConstants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(KeyConstants.Strings.JwtClaimIdentifiers.Rol, KeyConstants.Strings.JwtClaims.ApiAccess)
            });
        }

        /// <summary>
        /// ToUnixEpochDate
        /// </summary>
        /// <param name="date">date</param>
        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        /// <summary>
        /// ThrowIfInvalidOptions 
        /// </summary>
        /// <param name="options"></param>
        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options.IsNull())
                throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials.IsNull())
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator.IsNull())
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
