using ErrorOr;
using RESTAPI.ServiceErrors;

namespace RESTAPI.Models;

public class Person
{
    public const int MinNameLenght = 3;
    public const int MaxNameLenght = 30;

    public const int MinDescriptionLenght = 3;
    public const int MaxDescriptionLenght = 30;

    public Guid Id { get; }
    public string Name { get; }

    public string Description { get; }
    public DateTime StartDateTime { get; }

    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }

    public Person(Guid id,
                  string name,
                  string description,
                  DateTime starDateTime,
                  DateTime endDateTime,
                  DateTime lastModifiedDateTime)
    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = starDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
    }

    public static ErrorOr<Person> Create(string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        DateTime LastModifiedDateTime)
    {
        List<Error> errors = new();
        if (Name.Length is < MinNameLenght or > MaxNameLenght)
        {
            errors.Add(Errors.Person.InvalidName);
        }
        if (Description.Length is < MinDescriptionLenght or > MaxDescriptionLenght)
        {
            errors.Add(Errors.Person.InvalidDescription);
        }
        if (errors.Count > 0)
        {
            return errors;
        }
        return new Person(
        Guid.NewGuid(),
        Name,
        Description,
        StartDateTime,
        EndDateTime,
        DateTime.UtcNow
        );

    }
}