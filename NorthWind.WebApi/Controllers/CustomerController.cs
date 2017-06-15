using Microsoft.AspNetCore.Mvc;
using NorthWind.Domain.Commands.Handlers;
using NorthWind.Domain.Commands.Inputs.Customers;
using NorthWind.Domain.Repositories;
using NorthWind.Infrastructure.UnitOfWork;
using System.Threading.Tasks;

namespace NorthWind.WebApi.Controllers
{
    [Route("v1/customers")]
    public class CustomerController : BaseController
    {
        readonly CustomerHandler _customerHandler;
        readonly ICustomerRepository _customerRepository;

        public CustomerController(IUow uow, CustomerHandler customerHandler, ICustomerRepository customerRepository) : base(uow)
        {
            _customerHandler = customerHandler;
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateCustomerCommand command)
        {
            var result = _customerHandler.Handle(command);
            return await CreateResponse(result, _customerHandler.Notifications);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateCustomerCommand command)
        {
            var result = _customerHandler.Handle(command);
            return await CreateResponse(result, _customerHandler.Notifications);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = _customerHandler.Handle(id);

            return await CreateResponse(result, _customerHandler.Notifications);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {           
            return await CreateResponse(_customerRepository.GetById(id));
        }
    }
}