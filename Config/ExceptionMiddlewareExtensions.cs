using System.Net;
using Abyster_Test_Project.SharedKernel;
using Microsoft.AspNetCore.Diagnostics;

public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    { 
                        string message = contextFeature.Error.GetBaseException().Message;
                        await context.Response.WriteAsync(new ErrorDetail()
                        {
                            statusCode = context.Response.StatusCode,
                            message = message
                        }.ToString());
                    }
                });
            });
        }
    }