using Microsoft.AspNetCore.Mvc;
using NorthWind.Infrastructure.UnitOfWork;
using NorthWind.Shared.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.WebApi.Controllers
{
    public abstract class BaseController : Controller
    {
        readonly IUow _uow;
        public BaseController(IUow uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> CreateResponse(object result, IEnumerable<Notification> notifications = null)
        {
            var tsc = new TaskCompletionSource<IActionResult>();

            if (notifications == null || !notifications.Any())
            {
                try
                {
                    _uow.Commit();
                    tsc.SetResult(Ok(new
                    {
                        success = true,
                        data = result
                    }));
                }
                catch
                {
                    string message = "An error has occurred while trying to process your request. Please, try again later.";

                    tsc.SetResult(BadRequest(new
                    {
                        success = false,
                        errors = new[] { message }
                    }));
                }
            }
            else
            {
                tsc.SetResult(BadRequest(new
                {
                    success = false,
                    errors = notifications
                }));
            }

            return await tsc.Task;
        }
    }
}
