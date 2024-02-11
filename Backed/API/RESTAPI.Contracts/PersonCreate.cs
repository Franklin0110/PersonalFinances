namespace RESTAPI.Contracts.Person;

public record PersonCreate
(
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime
);
