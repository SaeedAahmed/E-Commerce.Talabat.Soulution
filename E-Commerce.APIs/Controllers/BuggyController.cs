using E_Commerce.APIs.Errors;
using E_Commerce.Core.Entities;
using E_Commerce.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        //not found data
        [HttpGet("NotFound")]
        public ActionResult NotFound()
        {
            var product = _dbContext.Products.Find(100);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(product);
        }
        //badrequest
        [HttpGet("BadRequest")]
        public ActionResult BadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        //Unauthorized
        [HttpGet("Unauthorized")]
        public ActionResult Unauthorized()
        {
            return Unauthorized(new ApiResponse(401));
        }
        //validationError
        [HttpGet("badRequest/{id}")]
        public ActionResult Validation(int id)
        {
            return Ok();
        }
        //Server Error
        [HttpGet("ServerError")]
        public ActionResult Server()
        {
            var product = _dbContext.Products.Find(100);
            var productToReturn = product.ToString();
            return Ok(productToReturn);
        }
    }
}
