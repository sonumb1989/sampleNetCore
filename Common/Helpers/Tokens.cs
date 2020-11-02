using Common.Authentication;
using Common.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class Tokens
    {
        public async Task<string> GenerateJwt(JwtFactory jwtFactory, AppUser user, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings, List<string> roles)
        {
            var response = await jwtFactory.GenerateEncodedToken(user, roles);
            return response;
        }
    }
}
