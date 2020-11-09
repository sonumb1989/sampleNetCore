using Common.Constants;
using GenericBizRunner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.Account;
using ServicesLayer.Dto;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Register User
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]AccountDto model, [FromServices]IActionServiceAsync<IRegisterAccountAction> service)
        {
            var result = await service.RunBizActionAsync<bool>(model);
            return Ok(result);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginDto model, [FromServices]IActionServiceAsync<ILoginAccountAction> service)
        {
            var response = await service.RunBizActionAsync<string>(model);
            return Ok(response);
        }
    }
}
