using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatAPI.Errors;

namespace TalabatAPI.Controllers
{
    [Route("/Errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            if(code == StatusCodes.Status404NotFound)
            {
                return NotFound(new ApiResponse(code));
            }
            else if(code == StatusCodes.Status401Unauthorized)
            {
                return Unauthorized(new ApiResponse(code));
            }
            else 
                return StatusCode(code);
        }
    }
}
