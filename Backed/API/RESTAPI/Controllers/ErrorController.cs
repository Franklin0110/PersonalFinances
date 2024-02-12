
using Microsoft.AspNetCore.Mvc;

namespace RESTAPI.Controllers;


public class ErrorHandeler : ControllerBase
{
    [Route("/error")]

    public IActionResult Error()
    {

        return Problem();
    }


}