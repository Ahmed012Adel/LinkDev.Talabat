using Azure;
using LinkDev.Talabat.Apis.Controller.Error;
using LinkDev.Talabat.Core.Application.Exceptions;
using System.Net;

namespace LinkDev.Talabat.APIs.Middlewares
{
    public class ExceptionHandelerMaddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandelerMaddelware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandelerMaddelware(RequestDelegate next , ILogger<ExceptionHandelerMaddelware> logger , IWebHostEnvironment env)
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
                #region Loggin

                if (_env.IsDevelopment())
                {
                    _logger.LogError(ex, ex.Message);

                }
                else
                {


                }

                #endregion

                await HandelExceptionAsync(httpContext, ex);
            }


        }

        private async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ApiResponse response;

            switch (ex)
            {

                case NotFoundException:

                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiResponse(404, ex.Message);

                    await httpContext.Response.WriteAsync(response.ToString());
                    Console.WriteLine(ex.Source);
                    break;



                case (BadRequestException):

                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiResponse(400, ex.Message);

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
                
                case UnAuthorizedException:

                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiResponse(401, ex.Message);

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                default:

                        response = _env.IsDevelopment() ? new ApiExeptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                                                          :
                                                          new ApiExeptionResponse((int)HttpStatusCode.InternalServerError);

                    httpContext.Response.StatusCode = response.StatusCode;
                    httpContext.Response.ContentType = "application/json";

                    await httpContext.Response.WriteAsync(response.ToString());


                    break;
            }
        }
    }
}
