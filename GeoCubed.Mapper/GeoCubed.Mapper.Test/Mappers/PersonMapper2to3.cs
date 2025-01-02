using GeoCubed.Mapper.Test.Models;

namespace GeoCubed.Mapper.Test.Mappers;

public class PersonMapper2to3 : IMapping<Person2, Person3>
{
    public Person3 Map(Person2 obj)
    {
        throw new NotImplementedException();
    }
}
