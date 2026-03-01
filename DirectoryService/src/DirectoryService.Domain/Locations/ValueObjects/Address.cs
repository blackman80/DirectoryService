using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Locations.ValueObjects;

public class Address
{
    private Address(string value)
    {
        Value = value;
    }

    public string Value { get; }

    // "В бд может быть несколько столбцов или jsonb" видимо это будет реализовываться позже
    public static Result<Address> Create(string value)
    {
        return new Address(value);
    }
}