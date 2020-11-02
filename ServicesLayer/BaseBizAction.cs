using AutoMapper;
using Common.Entities;
using Common.Extensions;
using Microsoft.AspNetCore.Identity;

namespace ServicesLayer.Action
{
    public class BaseBizAction
    {
        public IMapper AutoMapper;
        public UserManager<AppUser> UserManager;

        public BaseBizAction(IMapper autoMapper)
        {
            AutoMapper = autoMapper;
            UserManager = StartupExtensions.Resolve<UserManager<AppUser>>();
        }
    }
}
