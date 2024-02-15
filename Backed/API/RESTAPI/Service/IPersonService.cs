using ErrorOr;
using RESTAPI.Contracts;
using RESTAPI.Models;

namespace RESTAPI.Service;

public interface IPersonService
{
    void CreatePerson(Person person);
    ErrorOr<Person> GetPerson(Guid id);

    ErrorOr<Person> UpsertPerson(Person person);
    void DeletePerson(Guid id);
}