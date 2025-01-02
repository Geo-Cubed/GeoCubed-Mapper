using GeoCubed.Mapper.Test.Models;
namespace GeoCubed.Mapper.Test.Mappers;

public class PersonMapper2to1 : IMapping<Person2, Person1>
{
    public Person1 Map(Person2 obj)
    {
        var nameSplit = obj.FullName.Split(" ");
        return new Person1()
        {
            FirstName = nameSplit[0],
            LastName = nameSplit[1],
            Id = long.MinValue,
            DateOfBirth = obj.DateOfBirth,
        };
    }
}
