using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FwsManagement.Middleware
{
    public class MiddlewareDemo
    {
        //tham số cần có cho middleware
        private readonly RequestDelegate _next;

        // lấy giá trị của request delegate
        public MiddlewareDemo(RequestDelegate next)
        {
            _next = next;
        }

        // cho phép lấy dl,  sử lý thread-safe
        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareDemo>();
        }
    }
}
