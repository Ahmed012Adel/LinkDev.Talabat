using LinkDev.Talabat.Apis.Controller.Error;
using LinkDev.Talabat.Apis.Controller.Exceptions;
using System.Net;

namespace LinkDev.Talabat.APIs.Middlewares
{
    public class CustomExceptionHandelerMaddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandelerMaddelware> _logger;
        private readonly IWebHostEnvironment _env;

        public CustomExceptionHandelerMaddelware(RequestDelegate next , ILogger<CustomExceptionHandelerMaddelware> logger , IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync (HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

                //if(httpContext.Response.StatusCode ==(int) HttpStatusCode.NotFound)
                //{
                //    var response = new ApiResponse((int)HttpStatusCode.NotFound, $"the requested endpoint : {httpContext.Request.Path} is not found");
                //    await httpContext.Response.WriteAsync(response.ToString());

                //}
            }
            catch (Exception ex)
            {

                ApiResponse response;

                switch (ex) 
                {

                    case NotFoundException:

                        httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        httpContext.Response.ContentType = "application/json";

                         response = new ApiResponse(404, ex.Message);

                        await httpContext.Response.WriteAsync(response.ToString());
                        break;
                    default:

                        if (_env.IsDevelopment())
                        {
                            _logger.LogError(ex, ex.Message);
                            response = new ApiExeptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString());

                        }
                        else
                        {

                            response = new ApiExeptionResponse((int)HttpStatusCode.InternalServerError);

                        }
                        httpContext.Response.StatusCode = response.StatusCode;
                        httpContext.Response.ContentType = "application/json";

                        await httpContext.Response.WriteAsync(response.ToString());


                        break;
                }
            }


        }
    }
}
