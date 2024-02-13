using ErrorOr;

namespace RESTAPI.ServiceErrors;

public static class Errors
{

    public static class Person
    {
        public static Error NotFound => Error.NotFound(
                code: "Person.NotFound",
                description: "Person not found"
        );



    }

}