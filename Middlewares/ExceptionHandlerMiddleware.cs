using Microsoft.AspNetCore.Diagnostics;
using SERVICE_Layer.Models;
using System.Data.SqlClient;
using System.Net;
using System.Text.Json;
namespace API_Layer.Middlewares;

 
    public static class ExceptionHandlerMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Error while processing the request: {contextFeature.Error}");
                        await context.Response.WriteAsync(
                           JsonSerializer.Serialize(new ApiResponse()
                           {
                               StatusCode = HttpStatusCode.InternalServerError,
                               Message = "Internal Server Error."
                           }).ToString()
                        );
                    }
                });
            });
        }
    }
