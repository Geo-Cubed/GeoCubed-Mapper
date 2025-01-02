using GeoCubed.Mapper.Test.Models;

namespace GeoCubed.Mapper.Test.Mappers;

public class InjectedModelMapper1to2 : IMapping<InjectedModel1, InjectedModel2>
{
    private readonly InjectedService _service;

    public InjectedModelMapper1to2(InjectedService service)
    {
        ArgumentNullException.ThrowIfNull(service, nameof(service));
        this._service = service;
    }

    public InjectedModel2 Map(InjectedModel1 obj)
    {
        return new InjectedModel2()
        {
            SquaredValue = this._service.Square(obj.Value)
        };
    }
}
