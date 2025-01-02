using GeoCubed.Mapper.Test.Models;

namespace GeoCubed.Mapper.Test;

/// <summary>
/// Tests for the singular mappers.
/// </summary>
public class SingleMapperTests
{
    private readonly IMapping<Person1, Person2> _mapper;
    private readonly IMapping<Person2, Person1> _reverseMapper;
    private readonly IMapping<InjectedModel1, InjectedModel2> _injectedMapper;

    public SingleMapperTests(
        IMapping<Person1, Person2> mapper,
        IMapping<Person2, Person1> reverseMapper,
        IMapping<InjectedModel1, InjectedModel2> injectedMapper)
    {
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentNullException.ThrowIfNull(reverseMapper, nameof(reverseMapper));
        ArgumentNullException.ThrowIfNull(injectedMapper, nameof(injectedMapper));
        this._mapper = mapper;
        this._reverseMapper = reverseMapper;
        this._injectedMapper = injectedMapper;
    }

    /// <summary>
    /// Test that the single mapper will map as expected from one type to another.
    /// </summary>
    [Fact]
    public void Map()
    {
        var dbPerson = new Person1()
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
        var regPerson = new Person2()
        {
            FullName = "John Smith",
            DateOfBirth = DateTime.Now
        };

        var mapped = this._reverseMapper.Map(regPerson);

        Assert.Equal(regPerson.DateOfBirth, mapped.DateOfBirth);
        Assert.Equal(long.MinValue, mapped.Id);
        Assert.Equal(regPerson.FullName, mapped.FirstName + " " + mapped.LastName);
    }

    /// <summary>
    /// Test that services are correctly injected into the single mappers.
    /// </summary>
    [Fact]
    public void ServiceInjected()
    {
        var value = 10;
        var model1 = new InjectedModel1()
        {
            Value = value
        };

        var mapped = this._injectedMapper.Map(model1);

        Assert.Equal(Math.Pow(value, 2), mapped.SquaredValue);
    }
}