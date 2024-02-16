using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Contracts;
using RESTAPI.Contracts.Person;
using RESTAPI.Models;
using RESTAPI.Service;
namespace RESTAPI.Controllers;

[ApiController]
[Route("Person")]
public class PersonController : ApiController
{
    private readonly IPersonService _PersonService;

    public PersonController(IPersonService personService)
    {
        _PersonService = personService;
    }


    [HttpPost]
    public IActionResult CreatePerson(PersonCreate request)
    {
        var person = new Person(
        Guid.NewGuid(),
        request.Name,
        request.Description,
        request.StartDateTime,
        request.EndDateTime,
        DateTime.UtcNow
        );

        ErrorOr<Created> ServiceResponse = _PersonService.CreatePerson(person);
        if (ServiceResponse.IsError)
        {
            return Problem(ServiceResponse.Errors);
        }
        else
        {
            return CreatedAtAction(
                actionName: nameof(GetPerson),
                routeValues: new { id = person.Id },
                value: MapPersonResponse(person));
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetPerson(Guid id)
    {
        ErrorOr<Person> ServiceResponse = _PersonService.GetPerson(id);
        if (ServiceResponse.IsError)
        {
            return Problem(ServiceResponse.Errors);
        }
        else
        {
            return ServiceResponse.Match(
                Person => Ok(MapPersonResponse(Person)),
                errors => Problem(errors));
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertPerson(Guid id, UpSertPerson request)
    {
        var person = new Person(
          id,
          request.Name,
          request.Description,
          request.StartDateTime,
          request.EndDateTime,
          DateTime.UtcNow
      );

        ErrorOr<Updated> ServiceResponse = _PersonService.UpsertPerson(person);
        if (ServiceResponse.IsError)
        {
            return Problem(ServiceResponse.Errors);
        }
        else
        {
            return ServiceResponse.Match(
                 Updated => Ok(),
                 errors => Problem(errors));
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletePerson(Guid id)
    {
        ErrorOr<Deleted> ServiceResponse = _PersonService.DeletePerson(id);


        return ServiceResponse.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }


    private static PersonResponse MapPersonResponse(Person person)
    {
        return new PersonResponse(
                   person.Id,
                   person.Name,
                   person.Description,
                   person.StartDateTime,
                   person.EndDateTime,
                   person.LastModifiedDateTime
               );
    }
}

