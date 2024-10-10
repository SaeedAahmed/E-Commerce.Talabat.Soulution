using E_Commerce.APIs.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            if(code==404)
                return NotFound(new ApiResponse(404));
            else if(code==401)
                return Unauthorized(new ApiResponse(401));
            else
                return StatusCode(code);
        }
    }
}
