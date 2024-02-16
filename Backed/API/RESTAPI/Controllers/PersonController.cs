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
    //Dependency injection
    private readonly IPersonService _PersonService;
    public PersonController(IPersonService personService)
    {
        _PersonService = personService;
    }
    //CreatePerson Method when A post requested is made.
    [HttpPost]
    public IActionResult CreatePerson(PersonCreate request)
    {
        //We Create a new person
        ErrorOr<Person> requestToBreakFastResult = Person.Create(
        request.Name,
        request.Description,
        request.StartDateTime,
        request.EndDateTime,
        DateTime.UtcNow
        );
        if (requestToBreakFastResult.IsError) return Problem(requestToBreakFastResult.Errors);

        var person = requestToBreakFastResult.Value;
        //Calling a service from the dependency injection
        ErrorOr<Created> ServiceResponse = _PersonService.CreatePerson(person);

        if (ServiceResponse.IsError) return Problem(ServiceResponse.Errors);
        else
        {
            //Returns to the client, the name of the action, values and the whole JSON
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

