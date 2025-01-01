using GeoCubed.Mapper.Test.Models;

namespace GeoCubed.Mapper.Test;

/// <summary>
/// Tests for the global mapper class.
/// </summary>
public class GlobalMapperTests
{
    private readonly GlobalMapper _globalMapper;

    public GlobalMapperTests(GlobalMapper globalMapper)
    {
        ArgumentNullException.ThrowIfNull(globalMapper, nameof(globalMapper));
        this._globalMapper = globalMapper;
    }

    /// <summary>
    /// Tests that the global mapper will map with the first map method.
    /// </summary>
    [Fact]
    public void TestGlobalMapper()
    {
        var obj = new DbPerson()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = DateTime.Now,
        };

        var mapped = this._globalMapper.Map<DbPerson, RegPerson>(obj);

        Assert.Equal(obj.DateOfBirth, mapped.DateOfBirth);
        Assert.Equal(obj.FirstName + " " + obj.LastName, mapped.FullName);
    }

    /// <summary>
    /// Tests that the global mapper will map with the method containing only one generic parameter.
    /// </summary>
    [Fact]
    public void TestGlobalMapperOneGeneric()
    {
        var obj = new DbPerson()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = DateTime.Now,
        };

        var mapped = this._globalMapper.Map<RegPerson>(obj);

        Assert.Equal(obj.DateOfBirth, mapped.DateOfBirth);
        Assert.Equal(obj.FirstName + " " + obj.LastName, mapped.FullName);
    }

    /// <summary>
    /// Tests that the global mapper will map with the instance of the reverse mapper.
    /// </summary>
    [Fact]
    public void TestGlobalMapperReverse()
    {
        var obj = new RegPerson()
        {
            FullName = "John Smith",
            DateOfBirth = DateTime.Now,
        };

        var mapped = this._globalMapper.Map<RegPerson, DbPerson>(obj);

        Assert.Equal(obj.DateOfBirth, mapped.DateOfBirth);
        Assert.Equal(long.MinValue, mapped.Id);
        Assert.Equal(obj.FullName, mapped.FirstName + " " + mapped.LastName);
    }

    /// <summary>
    /// Tests that the global mapper will map with the reverse mapper with the method with only one generic.
    /// </summary>
    [Fact]
    public void TestGlobalMapperOneGenericReverse()
    {
        var obj = new RegPerson()
        {
            FullName = "John Smith",
            DateOfBirth = DateTime.Now,
        };

        var mapped = this._globalMapper.Map<DbPerson>(obj);

        Assert.Equal(obj.DateOfBirth, mapped.DateOfBirth);
        Assert.Equal(long.MinValue, mapped.Id);
        Assert.Equal(obj.FullName, mapped.FirstName + " " + mapped.LastName);
    }

    /// <summary>
    /// Tests that the mapper will throw a mapping exception when no mapper is found.
    /// </summary>
    [Fact]
    public void TestThrowsErrorOnNoMapper()
    {
        var exception = Assert.Throws<MappingException>(() => this._globalMapper.Map<RegPerson, UnMappedModel>(new RegPerson()));
        Assert.Equal(typeof(RegPerson), exception.FromType);
        Assert.Equal(typeof(UnMappedModel), exception.ToType);

        var mapping = typeof(IMapping<,>).MakeGenericType(typeof(RegPerson), typeof(UnMappedModel));
        var expected = string.Format("Cannot find the mapper of type {0} in the service provider.", mapping.Name);
        Assert.Equal(expected, exception.Message);
    }
    
    /// <summary>
    /// Tests that the mapper will throw a mapping exception when no mapper is found on the method with only one generic.
    /// </summary>
    [Fact]
    public void TestThrowsErrorOnNoMapperOneGeneric()
    {
        var exception = Assert.Throws<MappingException>(() => this._globalMapper.Map<UnMappedModel>(new RegPerson()));
        Assert.Equal(typeof(RegPerson), exception.FromType);
        Assert.Equal(typeof(UnMappedModel), exception.ToType);

        var mapping = typeof(IMapping<,>).MakeGenericType(typeof(RegPerson), typeof(UnMappedModel));
        var expected = string.Format("Cannot find the mapper of type {0} in the service provider.", mapping.Name);
        Assert.Equal(expected, exception.Message);
    }

    [Fact(Skip = "Have not figured out how to test this.")]
    public void TestThrowsErrorOnNoMapMethod()
    {
        // TODO: I'm not sure if I can test this as implementing the interface forces the method.
    }
    
    [Fact(Skip = "Have not figured out how to test this.")]
    public void TestThrowsErrorOnNoMapMethodOneGeneric()
    {
        // TODO: I'm not sure if I can test this as implementing the interface forces the method.
    }

    /// <summary>
    /// Tests a mapping exception is thrown when the mapper throws an error.
    /// </summary>
    [Fact]
    public void TestThrowsErrorOnMapMethodError()
    {
        var exception = Assert.Throws<MappingException>(() => this._globalMapper.Map<RegPerson, ThirdPerson>(new RegPerson()));

        Assert.Equal(typeof(RegPerson), exception.FromType);
        Assert.Equal(typeof(ThirdPerson), exception.ToType);

        var mapping = typeof(IMapping<,>).MakeGenericType(typeof(RegPerson), typeof(ThirdPerson));
        var expected = string.Format("An error occured while trying to run the 'Map' method on the mapper {0} see inner exception for more details", mapping.Name);
        Assert.Equal(expected, exception.Message);

        Assert.Equal(typeof(NotImplementedException), exception.InnerException.GetType());
    }
    
    /// <summary>
    /// Tests a mapping exception is thrown when the mapper throws an error on the method with only 1 generic argument.
    /// </summary>
    [Fact]
    public void TestThrowsErrorOnMapMethodErrorOneGeneric()
    {
        var exception = Assert.Throws<MappingException>(() => this._globalMapper.Map<ThirdPerson>(new RegPerson()));

        Assert.Equal(typeof(RegPerson), exception.FromType);
        Assert.Equal(typeof(ThirdPerson), exception.ToType);

        var mapping = typeof(IMapping<,>).MakeGenericType(typeof(RegPerson), typeof(ThirdPerson));
        var expected = string.Format("An error occured while trying to run the 'Map' method on the mapper {0} see inner exception for more details", mapping.Name);
        Assert.Equal(expected, exception.Message);

        Assert.Equal(typeof(NotImplementedException), exception.InnerException.GetType());
    }
}
