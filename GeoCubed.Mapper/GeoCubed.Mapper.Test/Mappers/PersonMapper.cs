using GeoCubed.Mapper.Test.Models;

namespace GeoCubed.Mapper.Test.Mappers;

public class PersonMapper : IMapping<DbPerson, RegPerson>
{
    public RegPerson Map(DbPerson obj)
    {
        return new RegPerson()
        {
            FullName = obj.FirstName + " " + obj.LastName,
            DateOfBirth = obj.DateOfBirth,
        };
    }
}