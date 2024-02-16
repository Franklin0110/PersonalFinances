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
        public static Error InvalidName => Error.Validation(
              code: "PersonName.NotValid",
              description: $"Person Name is not valid, the name needs to have between {Models.Person.MinNameLenght} and {Models.Person.MaxNameLenght}"
      );
        public static Error InvalidDescription => Error.Validation(
                  code: "PersonName.NotValid",
                  description: $"Person Name is not valid, the name needs to have between {Models.Person.MinDescriptionLenght} and {Models.Person.MaxDescriptionLenght}"
          );


    }

}