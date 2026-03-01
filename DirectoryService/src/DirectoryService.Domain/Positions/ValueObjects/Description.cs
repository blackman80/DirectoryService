using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Positions.ValueObjects;

public record Description
{
    public const int MAX_LENGTH = 1000;
    private Description(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Description> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length >= MAX_LENGTH)
        {
            return Result.Failure<Description>("Invalid description");
        }

        return new Description(value);
    }
}