using GeoCubed.Mapper.Common;

namespace GeoCubed.Mapper;

/// <summary>
/// Global mapper class capable of retrieving and calling mappers.
/// </summary>
public class GlobalMapper
{
    private readonly IServiceProvider _provider;
    private readonly string _mappingMethodName;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalMapper"/> class.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    public GlobalMapper(IServiceProvider provider)
    {
        this._provider = provider;

        // Get the name of the map method.
        var type = MappingHelper.CreateMappingType();
        this._mappingMethodName = type.GetMethods()[0].Name;
    }

    /// <summary>
    /// Maps an instance of an object to another object using the mappers.
    /// </summary>
    /// <typeparam name="From">The input type.</typeparam>
    /// <typeparam name="To">The output type.</typeparam>
    /// <param name="obj">The object to map.</param>
    /// <returns>The mapped object of type <see cref="{To}"/>.</returns>
    public To Map<From, To>(From obj)
        where From : class
        where To : class
    {
        var mappingType = MappingHelper.CreateMappingType<From, To>();
        return this.CreateAndRunMapping<To>(mappingType, obj);
    }

    /// <summary>
    /// Maps and instance of an object to another object using the mappers.
    /// </summary>
    /// <typeparam name="To">The type the object is being converted into.</typeparam>
    /// <param name="obj">The object to map.</param>
    /// <returns>The mapped object of type <see cref="{To}"/>.</returns>
    public To Map<To>(object obj)
        where To : class
    {
        var mapperType = MappingHelper.CreateMappingType(obj.GetType(), typeof(To));
        return this.CreateAndRunMapping<To>(mapperType, obj);
    }

    private To CreateAndRunMapping<To>(Type mapperType, object obj)
    {
        // Initialize the type.
        var instance = this._provider.GetService(mapperType);
        if (instance == null)
        {
            var exception = MappingExceptionBuilder
                .AMappingException()
                .WithMessage(MappingHelper.ERR_NO_MAPPER(mapperType))
                .WithFromType(obj.GetType())
                .WithToType(typeof(To))
                .Build();

            throw exception;
        }

        // Get the mapping method.
        var method = instance.GetType().GetMethod(this._mappingMethodName);
        if (method == null)
        {
            var exception = MappingExceptionBuilder
                .AMappingException()
                .WithMessage(MappingHelper.ERR_NO_MAP_METHOD(mapperType, this._mappingMethodName))
                .WithFromType(obj.GetType())
                .WithToType(typeof(To))
                .Build();

            throw exception;
        }

        // Call the mapping method.
        To result;
        try
        {
            result = (To)method.Invoke(instance, [obj]);
        }
        catch(Exception ex)
        {
            var exception = MappingExceptionBuilder
                .AMappingException()
                .WithMessage(MappingHelper.ERR_ON_MAP(mapperType, this._mappingMethodName))
                .WithFromType(obj.GetType())
                .WithToType(typeof(To))
                .WithInnerException(ex.InnerException)
                .Build();

            throw exception;
        }

        return result;
    }
}