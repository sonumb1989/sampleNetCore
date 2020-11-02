using AutoMapper;
using Common.Entities;
using GenericBizRunner;
using Microsoft.AspNetCore.Identity;
using ServicesLayer.Dto;
using System;
using System.Threading.Tasks;

namespace ServicesLayer.Account
{
    public class LoginAccountAction : BizActionStatus, IRegisterAccountAction
    {
        public IMapper AutoMapper;
        public UserManager<AppUser> UserManager;


        public LoginAccountAction(IMapper autoMapper, UserManager<AppUser> userManager)
        {
            AutoMapper = autoMapper;
            UserManager = userManager;
        }


        public async Task<bool> BizActionAsync(AccountDto request)
        {
            var userIdentity = AutoMapper.Map<AppUser>(request);
            userIdentity.Id = Guid.NewGuid().ToString();
            var result = await UserManager.CreateAsync(userIdentity, request.Password);

            if (!result.Succeeded)
            {
                return false;
            }
            else
            {
                var user = await UserManager.FindByNameAsync(userIdentity.UserName);
                try
                {
                    await UserManager.AddToRoleAsync(user, "Admin");
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
