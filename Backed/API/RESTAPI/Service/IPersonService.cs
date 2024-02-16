using ErrorOr;
using RESTAPI.Contracts;
using RESTAPI.Models;

namespace RESTAPI.Service;

public interface IPersonService
{
    ErrorOr<Created> CreatePerson(Person person);
    ErrorOr<Person> GetPerson(Guid id);
    ErrorOr<Updated> UpsertPerson(Person person);
    ErrorOr<Deleted> DeletePerson(Guid id);
}