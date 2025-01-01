namespace GeoCubed.Mapper.Common;

internal static class MappingHelper
{
    internal static Type CreateMappingType() => typeof(IMapping<,>);

    internal static Type CreateMappingType<Tin, Tout>()
    {
        var inputType = typeof(Tin);
        var outputType = typeof(Tout);
        return CreateMappingType().MakeGenericType(inputType, outputType);
    }

    internal static Type CreateMappingType(Type from, Type to)
    {
        return CreateMappingType().MakeGenericType(from, to);
    }

    internal static string ERR_NO_MAPPER(Type mapperType)
        => string.Format("Cannot find the mapper of type {0} in the service provider.", mapperType.Name);

    internal static string ERR_NO_MAP_METHOD(Type mapperType, string methodName)
        => string.Format("Cannot find the '{0}' method on the mapper {1}.", methodName, mapperType.Name);

    internal static string ERR_ON_MAP(Type mapperType, string methodName)
        => string.Format("An error occured while trying to run the '{0}' method on the mapper {1} see inner exception for more details", methodName, mapperType.Name);
}
