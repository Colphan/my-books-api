using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using my_books_api.Data.ViewModels;

namespace my_books_api.Data.ViewModels
{
    public class CustomExceptionMiddleware
    {
    private readonly RequestDelegate _next;


    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.ContentType = "application/json";

                    var response = new ErrorVM()
                    {
                         StatusCode = httpContext.Response.StatusCode,
                            Message = "Internal server error from the custom middleware",
                            Path = "path-goes-here"
                    };

                    return httpContext.Response.WriteAsync(response.ToString());
    }
 }
}