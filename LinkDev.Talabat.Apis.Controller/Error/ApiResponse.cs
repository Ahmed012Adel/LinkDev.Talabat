using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Apis.Controller.Error
{
    internal class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int statusCode , string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefultMessageForStatusCode(StatusCode);
        }

        private string? GetDefultMessageForStatusCode(int code)
        {
            return code switch
            {
                400 => "a bad request,you have made",
                401 => "Authorized, you are not",
                404 => "Resource was not found",
                500 => "Errors are the path to the dark",
                _ => null
            };
        }
    }
}
