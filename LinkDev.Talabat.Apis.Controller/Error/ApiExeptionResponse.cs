using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Apis.Controller.Error
{
    public class ApiExeptionResponse : ApiResponse
    {
        public string? Details { get; set; }

        public ApiExeptionResponse(int code , string? message =null , string? details = null) : base(code , message)
        {
            Details = details;
        }

        
    }
}
