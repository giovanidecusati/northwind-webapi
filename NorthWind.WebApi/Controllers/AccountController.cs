using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Domain.Commands.Handlers;
using NorthWind.Domain.Commands.Inputs.Account;
using NorthWind.Infrastructure.UnitOfWork;
using System.Threading.Tasks;

namespace NorthWind.WebApi.Controllers
{
    [Route("v1/account")]
    public class AccountController : BaseController
    {
        readonly AccountHandler _accountHandler;

        public AccountController(IUow uow, AccountHandler accountHandler) : base(uow)
        {
            _accountHandler = accountHandler;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterCommand command)
        {
            var result = _accountHandler.Handle(command);
            return await CreateResponse(result, _accountHandler.Notifications);
        }
    }
}
