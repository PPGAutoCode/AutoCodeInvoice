
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Types;
using ProjectName.ControllersExceptions;

namespace ProjectName.Controllers
{
    public static class SafeExecutor
    {
        public static async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "An error occurred while processing your request.",
                    Status = 500,
                    Detail = ex.Message
                };
                return new ObjectResult(problemDetails)
                {
                    StatusCode = 500
                };
            }
        }
    }
}
