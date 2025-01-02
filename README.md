# GeoCubed Mapper
 
This is a mapping library I wrote where you write your own custom mappers which will then be searched for and registered to the service collection

The mappers can then be individually injected into your class or accessed via the global mapper which will search for the correct mapper for the object provided

## Quick Start

##### 1. Add a reference to the `GeoCubed.Mapper.csproj` file 
This file can be found at `/GeoCubed.Mapper/GeoCubed.Mapper/GeoCubed.Mapper.csproj`

##### 2. Write some mappers
To write a mapper a public class needs creating that implements the `GeoCubed.Mapper.IMapping<,>` interface. The map method can then be written to perform the mapping.

Example Mapper:
```csharp
public class PersonMapper : IMapping<Person1, Person2>
{
    private readonly SomeService _service;

    public PersonMapper(SomeService service)
    {
        ArgumentNullException.ThrowIfNull(service, nameof(service));
        this._service = service;
    }

    public Person2 Map(Person1 obj)
    {
        return new Person2()
        {
            FirstName = obj.FirstName,
            LastName = obj.LastName,
            DateOfBirth = obj.DateOfBirth,
            Age = this._service.CalculateAge(obj.DateOfBirth),
        };
    }
} 
```

Note:
- A mapper will need to be written for each mapping you wish to support
- The mappers support dependancy injection

##### 3. Add the mappers to the service collection
This will normally be done in Startup.cs or Program.cs

```csharp
GeoCubed.Mapper.MapperServiceRegistration.AddMapper(serivces, Assembly.GetExecutingAssembly());
```
or
```csharp
services.AddMapper(Assembly.GetExecutingAssembly());
```

Note:
The assembly does not have to be specified but it will default to `Assembly.GetCallingAssembly()`
All assemblies where mappers are held will need registering so if you have mapper1 in assembly1 and mapper2 in assembly2 you will need to do the following

```csharp
services.AddMapper(Assembly1);
services.AddMapper(Assembly2);
```

## Using the mappers

- ### Individual Mappers
Mappers can be injected individulally and mapped directly

##### 1. Inject the mapper of choice
##### 2. Call the map method
```csharp
public class ExampleClass
{
    // * STEP 1 *
    private readonly IMapping<Person1, Person2> _mapping;

    public ExampleClass(IMapping<Person1, Person2> mapping)
    {
        this._mapping = mapping;
    }
    // * END STEP 1 *

    // * STEP 2 *
    public void SomeMethod()
    {
        var person1 = new Person1() 
        {
            FirstName = "Geo",
            LastName = "Cubed",
        };

        var person2 = this._mapping.Map(person1);
    }
    // * END STEP 2 *
}
```

- ### Global Mapper
Another option is to use the global mapper, this exposes a generic map method which will search for and invoke the correct mapper (providing one exists)

##### 1. Inject the global mapper
##### 2. Call the map method
There are then two ways to call the mapping:
- Specifying both the To and From types
- Just using the type you want the object to map to.

```csharp
public class ExampleClass
{
    // * STEP 1 *
    private readonly GlobalMapper _mapper;

    public ExampleClass(GlobalMapper mapper)
    {
        this._mapper = mapper;
    }
    // * END STEP 1 *

    // * STEP 2 *
    public void SpecifyBothTypes()
    {
        var person1 = new Person1() 
        {
            FirstName = "Geo",
            LastName = "Cubed",
        };

        // Specify the type you're mapping from and the type you're mapping to.
        var person2 = this._mapper.Map<Person1, Person2>(person1);
    }

    public void SpecifyToTypeOnly()
    {
        var person1 = new Person1() 
        {
            FirstName = "Geo",
            LastName = "Cubed",
        };

        // Specify just the type you want to map to.
        var person2 = this._mapper.Map<Person2>(person1);
    }
    // * END STEP 2 *
}
```

## Test Project

Located at `GeoCubed.Mapper/GeoCubed.Mapper.Test` there is a xUnit test projct file `GeoCubed.Mapper.Test.csproj`

This project just contains unit tests used to validate the functionality of the mappers

This project can be ignored as it doesn't have purpose other than validating the mappers work as expected
