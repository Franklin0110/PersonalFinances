using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace RESTAPI.Controllers;


[ApiController]
[Route("[Person]")]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> Errors)
    {
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