using LinkDev.Talabat.Apis.Controller.Controllers.BaseController;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Apis.Controller.Controllers.Buggy
{
    public class BuggyController : ApiControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new {StatusCode = 404, Message = "Not found"});
        }

        [HttpGet("servererror")]
        public IActionResult GetServerErorr()
        {
            throw new Exception();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new { StatusCode = 400, Message = "Bad Request" });
        }
        [HttpGet("badrequest/{id}")]
        public IActionResult GetValidationErorr()
        {
            return Ok();
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnAuthorized()
        {
            return Unauthorized(new { StatusCode = 401, Message = "Unauthorized" });
        }

        [HttpGet("forbidden")]
         public IActionResult GetForbiddenrequest()
        {
            return Ok();
        }


    }
}
