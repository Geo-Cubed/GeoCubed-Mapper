using System.Reflection;
using GeoCubed.Mapper.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GeoCubed.Mapper;

/// <summary>
/// Service registration class for registering services provided by the mapper.
/// </summary>
public static class MapperServiceRegistration
{
    private static List<Assembly> _assemblies = new ();

    /// <summary>
    /// Adds the services provided by the mapper to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="mappingAssembly">The assembly where the mappers are held.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddMapper(this IServiceCollection services, Assembly? mappingAssembly = null)
    {
        services.TryAddSingleton<GlobalMapper>();

        mappingAssembly ??= Assembly.GetCallingAssembly();
        if (!_assemblies.Contains(mappingAssembly))
        {
            // Stop the assembly from being used again.
            _assemblies.Add(mappingAssembly);

            // Add the mappers to the service collection.
            var assemblyTypes = mappingAssembly.GetTypes();
            for (int i = 0; i < assemblyTypes.Length; ++i)
            {
                var mapper = assemblyTypes[i];

                var genericType = mapper.GetInterface(MappingHelper.CreateMappingType().Name);
                if (genericType != null)
                {
                    services.AddScoped(genericType, mapper);
                }
            }
        }

        return services;
    }
}
