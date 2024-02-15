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

        _PersonService.CreatePerson(person);

        var response = new PersonResponse(
            person.Id,
            person.Name,
            person.Description,
            person.StartDateTime,
            person.EndDateTime,
            person.LastModifiedDateTime
        );

        return CreatedAtAction(
            actionName: nameof(GetPerson),
            routeValues: new { id = person.Id },
            value: response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetPerson(Guid id)
    {
        ErrorOr<Person> personResult = _PersonService.GetPerson(id);
        return personResult.Match(
            Person => Ok(MapPersonResponse(Person)),
            errors => Problem(errors));

        //  var person = personResult.Value;
        //  PersonResponse response = MapPersonResponse(person);

        //  return Ok(response);
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
        ErrorOr<Person> personResult = _PersonService.UpsertPerson(person);

        return personResult.Match(
             Person => Ok(MapPersonResponse(Person)),
             errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletePerson(Guid id)
    {
        _PersonService.DeletePerson(id);
        return NoContent();
    }
}
