using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatApi.Errors;

namespace TalabatApi.Controllers
{
    [Route("Error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ErrorController : ControllerBase
    {
        public ActionResult Eerror(int code)
        {
            return NotFound(new ApiErrorResponse(code));
        }
    }
}
