using AutoMapper;
using Common.Authentication;
using Common.Entities;
using Common.Helpers;
using GenericBizRunner;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServicesLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesLayer.Account
{
    public class LoginAccountAction : BizActionStatus, ILoginAccountAction
    {
        public IMapper AutoMapper;
        public UserManager<AppUser> UserManager;
        public IOptions<JwtIssuerOptions> JwtOptions;

        public LoginAccountAction(IMapper autoMapper, UserManager<AppUser> userManager, IOptions<JwtIssuerOptions> jwtOptions)
        {
            AutoMapper = autoMapper;
            UserManager = userManager;
            JwtOptions = jwtOptions;
        }


        public async Task<string> BizActionAsync(LoginDto request)
        {
            var result = string.Empty;
            Tokens token = new Tokens();
            JwtFactory jwtFactory = new JwtFactory(JwtOptions);

            var username = request.UserName;
            var password = request.Password;

            var userToVerify = await UserManager.FindByNameAsync(username);

            if (userToVerify == null)
            {
                return result;
            }

            // check the credentials
            if (await UserManager.CheckPasswordAsync(userToVerify, password))
            {
                var roles = await UserManager.GetRolesAsync(userToVerify);
                result = await token.GenerateJwt(jwtFactory, userToVerify, JwtOptions.Value, new JsonSerializerSettings { Formatting = Formatting.Indented }, (List<string>)roles);
            }

            return result;
        }
    }
}
