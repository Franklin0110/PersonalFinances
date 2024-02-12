using RESTAPI.Moderls;

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

    public Person GetPerson(Guid id)
    {
        return _person[id];
    }

    public Person UpsertPerson(Person person)
    {
        _person[person.Id] = person;
        return _person[person.Id];
    }
}