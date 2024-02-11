namespace RESTAPI.Contracts;

public record PersonResponse
(
        Guid Id,
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        DateTime LastModifiedDateTime
);
