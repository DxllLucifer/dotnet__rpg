using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet__rpg.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger? _logger;
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) => _logger = logger;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next){

            try
            {
            await next(context);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ProblemDetails problem = new(){
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "server error",
                    Title = "server error",
                    Detail = "An internal server has occoured"
                };
                var json = JsonSerializer.Serialize(problem);
                await context.Response.WriteAsync(json);
                context.Response.ContentType = "application/json";
            }
        } 
    }
}