using RESTAPI.Moderls;

namespace RESTAPI.Service;

public class PersonService : IPersonService
{
    private readonly Dictionary<Guid, Person> _person = new();
    public void CreatePerson(Person person)
    {
        _person.Add(person.Id, person);
    }

    public Person GetPerson(Guid id)
    {
        return _person[id];
    }

}