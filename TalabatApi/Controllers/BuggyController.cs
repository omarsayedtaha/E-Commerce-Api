using CoreLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Data;
using TalabatApi.Errors;

namespace TalabatApi.Controllers
{
    
    public class BuggyController : ApiBaseController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("NotFound")]
        public ActionResult GetNotFoundResponse()
        {
            var result = _dbContext.Products.Find(100);
            if (result == null) return NotFound(new ApiErrorResponse(404));

            return Ok(result);
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError() 
        { 
          var res = _dbContext.Products.Find(100);
            return Ok(res.ToString());
        }


        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(400)); 
        }


        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok(new ApiValidationErrorResponse());
        }
    }
}
