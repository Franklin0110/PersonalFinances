using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;
using RESTAPI.Models;
using RESTAPI.ServiceErrors;

namespace RESTAPI.Service;

public class PersonService : IPersonService
{
    private static readonly Dictionary<Guid, Person> _person = new();
    public ErrorOr<ErrorOr.Created> CreatePerson(Person person)
    {
        _person.Add(person.Id, person);
        return Result.Created;
    }

    public ErrorOr<Deleted> DeletePerson(Guid id)
    {
        _person.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Person> GetPerson(Guid id)
    {
        if (_person.TryGetValue(id, out var person1))
        {
            return person1;
        }
        return Errors.Person.NotFound;
    }

    public ErrorOr<Updated> UpsertPerson(Person person)
    {
        _person[person.Id] = person;
        return Result.Updated;
    }
}