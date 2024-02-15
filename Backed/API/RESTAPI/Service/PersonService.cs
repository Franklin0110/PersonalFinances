using ErrorOr;
using RESTAPI.Models;
using RESTAPI.ServiceErrors;

namespace RESTAPI.Service;

public class PersonService : IPersonService
{
    private static readonly Dictionary<Guid, Person> _person = new();
    public void CreatePerson(Person person)
    {
        _person.Add(person.Id, person);
    }

    public void DeletePerson(Guid id)
    {
        _person.Remove(id);
    }

    public ErrorOr<Person> GetPerson(Guid id)
    {
        if (_person.TryGetValue(id, out var person1))
        {
            return person1;
        }
        return Errors.Person.NotFound;
    }

    public ErrorOr<Person> UpsertPerson(Person person)
    {
        _person[person.Id] = person;
        return _person[person.Id];
    }
}