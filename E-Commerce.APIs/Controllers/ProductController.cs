using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Core.Specifications.Specification.Classes;
using E_Commerce.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductController(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync()
        {
            var spec = new ProductBrandCategorySpecification();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(products);
        }
 
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductAsync(int id)
        {
            var spec = new ProductBrandCategorySpecification(id);
            var product = await _productRepo.GetSpecAsync(spec);
            if (product == null)
            {
                return NotFound(new { Message = "Not Found", StatusCode = 404 });
            }
            return Ok(product);
        }
    }
}
