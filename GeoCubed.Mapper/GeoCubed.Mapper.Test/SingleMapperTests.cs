using GeoCubed.Mapper.Test.Models;

namespace GeoCubed.Mapper.Test;

/// <summary>
/// Tests for the singular mappers.
/// </summary>
public class SingleMapperTests
{
    private readonly IMapping<DbPerson, RegPerson> _mapper;
    private readonly IMapping<RegPerson, DbPerson> _reverseMapper;

    public SingleMapperTests(IMapping<DbPerson, RegPerson> mapper, IMapping<RegPerson, DbPerson> reverseMapper)
    {
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentNullException.ThrowIfNull(reverseMapper, nameof(reverseMapper));
        this._mapper = mapper;
        this._reverseMapper = reverseMapper;
    }

    /// <summary>
    /// Test that the single mapper will map as expected from one type to another.
    /// </summary>
    [Fact]
    public void Map()
    {
        var dbPerson = new DbPerson()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = DateTime.Now,
        };

        var mapped = this._mapper.Map(dbPerson);

        Assert.Equal(dbPerson.DateOfBirth, mapped.DateOfBirth);
        Assert.Equal(dbPerson.FirstName + " " + dbPerson.LastName, mapped.FullName);
    }

    /// <summary>
    /// Checks that the single mapper will map correctly for a reverse mapper.
    /// </summary>
    [Fact]
    public void ReverseMap()
    {
        var regPerson = new RegPerson()
        {
            FullName = "John Smith",
            DateOfBirth = DateTime.Now
        };

        var mapped = this._reverseMapper.Map(regPerson);

        Assert.Equal(regPerson.DateOfBirth, mapped.DateOfBirth);
        Assert.Equal(long.MinValue, mapped.Id);
        Assert.Equal(regPerson.FullName, mapped.FirstName + " " + mapped.LastName);
    }
}