using GeoCubed.Mapper.Test.Models;

namespace GeoCubed.Mapper.Test.Mappers;

public class PersonMapper1to2 : IMapping<Person1, Person2>
{
    public Person2 Map(Person1 obj)
    {
        return new Person2()
        {
            FullName = obj.FirstName + " " + obj.LastName,
            DateOfBirth = obj.DateOfBirth,
        };
    }
}