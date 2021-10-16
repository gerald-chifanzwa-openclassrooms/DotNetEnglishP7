using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Repositories;

namespace WebApi.Middleware
{
    public class ExceptionHandler : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                await next();
            }
            catch (EntityNotFoundException)
            {
                context.HttpContext.Response.StatusCode = 404;
                await context.HttpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Detail = "The item with the given id could not be found in the database.",
                    Title = "Item not found",
                    Type = "NotFound",
                    Status = 404
                });
            }
        }
    }
}
