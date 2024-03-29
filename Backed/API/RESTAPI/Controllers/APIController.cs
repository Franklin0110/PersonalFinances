using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RESTAPI.Controllers;


[ApiController]
[Route("[Person]")]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> Errors)
    {
        if (Errors.All(e => e.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in Errors)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem(modelStateDictionary);
        }
        if (Errors.All(e => e.Type == ErrorType.Unexpected))
        {
            return Problem();
        }

        var firstError = Errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError

        };
        return Problem(statusCode: statusCode, title: firstError.Description);


    }

}