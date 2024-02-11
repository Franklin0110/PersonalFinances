using RESTAPI.Contracts;
using RESTAPI.Moderls;

namespace RESTAPI.Service;

public interface IPersonService
{
    void CreatePerson(Person person);
    Person GetPerson(Guid id);
}