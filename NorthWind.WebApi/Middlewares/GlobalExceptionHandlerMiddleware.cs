using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using Newtonsoft.Json;

namespace NorthWind.WebApi.Middlewares
{
    public static class GlobalExceptionHandlerMiddleware
    {
        public static void Handle(IApplicationBuilder app, bool debug = false)
        {
            app.Run(
                async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var ex = context.Features.Get<IExceptionHandlerFeature>();

                    string message = "An error has occurred while trying to process your request. Please, try again later.";

                    if (ex != null)
                    {
                        string err = string.Empty;

                        if (debug)
                            err = JsonConvert.SerializeObject(
                                new
                                {
                                    success = false,
                                    errors = new[] { message },
                                    exception = ex.Error.Message,
                                    exceptionStack = ex.Error.StackTrace
                                });
                        else
                            err = JsonConvert.SerializeObject(
                                new
                                {
                                    success = false,
                                    errors = new[] { message }
                                });

                        await context.Response.WriteAsync(err).ConfigureAwait(false);
                    }
                });
        }
    }
}