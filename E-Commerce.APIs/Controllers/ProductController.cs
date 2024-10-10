using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.APIs.Errors;
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
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo
            , IGenericRepository<ProductBrand> BrandRepo
            , IGenericRepository<ProductType> TypeRepo
            , IMapper mapper)
        {
            _productRepo = productRepo;
            _brandRepo = BrandRepo;
            _typeRepo = TypeRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync()
        {
            var spec = new ProductBrandCategorySpecification();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable< ProductDto>>(products));
        }
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductAsync(int id)
        {
            var spec = new ProductBrandCategorySpecification(id);
            var product = await _productRepo.GetSpecAsync(spec);
            if (product == null)
            {
                return NotFound(new ApiResponse(405));
            }
            return Ok(_mapper.Map<Product , ProductDto>(product));
        }
        [HttpGet("GetBrands")]
        public async Task<ActionResult<ProductBrand>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("GetCategories")]
        public async Task<ActionResult<ProductType>> GetCategories()
        {
            var categories = await _typeRepo.GetAllAsync();
            return Ok(categories);
        }
    }
}
