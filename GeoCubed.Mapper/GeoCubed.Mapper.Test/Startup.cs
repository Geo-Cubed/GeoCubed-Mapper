using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace GeoCubed.Mapper.Test;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<InjectedService>();
    }
}