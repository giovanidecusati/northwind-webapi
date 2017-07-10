using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NorthWind.Domain.Commands.Handlers;
using NorthWind.Domain.Commands.Inputs.Products;
using NorthWind.Domain.Commands.Results.Products;
using NorthWind.Domain.Repositories;
using NorthWind.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthWind.WebApi.Controllers
{
    [Route("v1/products")]
    public class ProductController : BaseController
    {
        readonly ProductHandler _productHandler;
        readonly IProductRepository _productRepository;
        readonly IMemoryCache _memoryCache;

        public ProductController(
            IUow uow, 
            ProductHandler productHandler, 
            IProductRepository productRepository, 
            IMemoryCache memoryCache) : base(uow)
        {
            _productHandler = productHandler;
            _productRepository = productRepository;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateProductCommand command)
        {
            var result = _productHandler.Handle(command);
            return await CreateResponse(result, _productHandler.Notifications);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommand command)
        {
            var result = _productHandler.Handle(command);
            return await CreateResponse(result, _productHandler.Notifications);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = _productHandler.Handle(id);

            return await CreateResponse(result, _productHandler.Notifications);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            return await CreateResponse(_productRepository.GetById(id));
        }

        [HttpGet]
        [Route("forsale")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsForSale()
        {
            var cacheKey = "GetProductsForSale";

            IEnumerable<ProductsForSalesResult> result = new List<ProductsForSalesResult>();

            if (_memoryCache.TryGetValue(cacheKey, out result))
            {
                return await CreateResponse(result);
            }
            else
            {
                result = _productRepository.GetProductsForSales();
                _memoryCache.Set<IEnumerable<ProductsForSalesResult>>(cacheKey, result);
                return await CreateResponse(result);
            }
        }
    }
}