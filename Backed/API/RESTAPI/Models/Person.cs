namespace RESTAPI.Moderls;

public class Person
{
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

}