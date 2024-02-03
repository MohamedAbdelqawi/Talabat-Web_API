using System.Net;
using Talabat.Apis.Errors;

namespace Talabat.Apis.Middlewares
{
    public class ExcepetionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExcepetionMiddleware> logger;
        private readonly IHostEnvironment environment;

        public ExcepetionMiddleware(RequestDelegate next, ILogger<ExcepetionMiddleware> logger, IHostEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;

        }


        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = environment.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);


                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
