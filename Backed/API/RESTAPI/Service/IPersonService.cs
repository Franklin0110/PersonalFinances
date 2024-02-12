using RESTAPI.Contracts;
using RESTAPI.Moderls;

namespace RESTAPI.Service;

public interface IPersonService
{
    void CreatePerson(Person person);
    Person GetPerson(Guid id);

    Person UpsertPerson(Person person);
    void DeletePerson(Guid id);
}