using GenericBizRunner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.Account;
using ServicesLayer.Dto;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
