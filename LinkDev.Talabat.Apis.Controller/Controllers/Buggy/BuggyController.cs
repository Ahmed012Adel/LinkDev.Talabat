using LinkDev.Talabat.Apis.Controller.Controllers.BaseController;
using LinkDev.Talabat.Apis.Controller.Error;
using LinkDev.Talabat.Apis.Controller.Exceptions;
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
            throw new NotFoundException("notfound" , 3);
            //return NotFound(new ApiResponse(404));
        }

        [HttpGet("servererror")]
        public IActionResult GetServerErorr()
        {
            throw new Exception();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public IActionResult GetValidationErorr()
        {
            return Ok();
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnAuthorized()
        {
            return Unauthorized(new ApiResponse(401));
        }

        [HttpGet("forbidden")]
         public IActionResult GetForbiddenrequest()
        {
            return Ok();
        }


    }
}
