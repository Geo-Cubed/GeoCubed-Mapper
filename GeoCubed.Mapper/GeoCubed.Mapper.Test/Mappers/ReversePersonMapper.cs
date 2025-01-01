using GeoCubed.Mapper.Test.Models;
namespace GeoCubed.Mapper.Test.Mappers;

public class ReversePersonMapper : IMapping<RegPerson, DbPerson>
{
    public DbPerson Map(RegPerson obj)
    {
        var nameSplit = obj.FullName.Split(" ");
        return new DbPerson()
        {
            FirstName = nameSplit[0],
            LastName = nameSplit[1],
            Id = long.MinValue,
            DateOfBirth = obj.DateOfBirth,
        };
    }
}
