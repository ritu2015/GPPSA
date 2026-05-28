using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPPSA.Api.Middlewares;

namespace GPPSA.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddelware>();
        }
        
    }
}