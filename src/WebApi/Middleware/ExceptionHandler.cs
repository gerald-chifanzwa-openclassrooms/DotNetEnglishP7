using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Repositories;

namespace WebApi.Middleware
{
    public class ExceptionHandler : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is EntityNotFoundException)
            {
                context.HttpContext.Response.StatusCode = 404;
                context.Result = new NotFoundObjectResult(new ProblemDetails
                {
                    Detail = "The item with the given id could not be found in the database.",
                    Title = "Item not found",
                    Type = "NotFound",
                    Status = 404
                });
                context.ExceptionHandled = true;
            }
        }
    }
}
