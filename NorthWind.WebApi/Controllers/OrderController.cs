using Microsoft.AspNetCore.Mvc;
using NorthWind.Domain.Commands.Handlers;
using NorthWind.Domain.Commands.Inputs.Orders;
using NorthWind.Domain.Repositories;
using NorthWind.Infrastructure.UnitOfWork;
using System.Threading.Tasks;

namespace NorthWind.WebApi.Controllers
{
    [Route("v1/orders")]
    public class OrderController : BaseController
    {
        readonly OrderHandler _orderHandler;
        readonly IOrderRepository _orderRepository;

        public OrderController(IUow uow, OrderHandler orderHandler, IOrderRepository orderRepository) : base(uow)
        {
            _orderHandler = orderHandler;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterOrderCommand command)
        {
            var result = _orderHandler.Handle(command);
            return await CreateResponse(result, _orderHandler.Notifications);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {           
            return await CreateResponse(_orderRepository.GetById(id));
        }
    }
}