using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.APIs.Errors;
using E_Commerce.APIs.Helpers;
using E_Commerce.APIs.Helpers.Paginations;
using E_Commerce.Core.Entities.Products;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Core.Specifications.Specification.Classes.ProductSpec;
using E_Commerce.Repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductDto>>>>GetProductsAsync([FromQuery] ProductSpecificationParameters productParameters)
        {
            var spec = new ProductBrandCategorySpecification(productParameters);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            var countAsync = new ProductCountForSpecification(productParameters);
            var count = await _unitOfWork.Repository<Product>().GetCountAsync(countAsync);
            return Ok(new Pagination<ProductDto>(productParameters.PageIndex , productParameters.PageSize ,count, data));
        }


        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductAsync(int id)
        {
            var spec = new ProductBrandCategorySpecification(id);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(spec);
            if (product == null)
            {
                return NotFound(new ApiResponse(405));
            }
            return Ok(_mapper.Map<Product , ProductDto>(product));
        }
        [HttpGet("GetBrands")]
        public async Task<ActionResult<ProductBrand>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("GetCategories")]
        public async Task<ActionResult<ProductCategory>> GetCategories()
        {
            var categories = await _unitOfWork.Repository<ProductCategory>().GetAllAsync();
            return Ok(categories);
        }
    }
}
