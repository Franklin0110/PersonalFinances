using Microsoft.AspNetCore.Mvc;
using RESTAPI.Contracts;
using RESTAPI.Contracts.Person;
using RESTAPI.Moderls;
using RESTAPI.Service;
namespace RESTAPI.Controllers;

[ApiController]
[Route("Person")]
public class APIController : ControllerBase
{
    private readonly IPersonService _PersonService;

    public APIController(IPersonService personService)
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
        Person person = _PersonService.GetPerson(id);
        var response = new PersonResponse(
           person.Id,
           person.Name,
           person.Description,
           person.StartDateTime,
           person.EndDateTime,
           person.LastModifiedDateTime
       );

        return Ok(response);
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
        _PersonService.UpsertPerson(person);

        // TODO: return 201 if a new breakfast was created
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletePerson(Guid id)
    {
        _PersonService.DeletePerson(id);
        return NoContent();
    }
}
