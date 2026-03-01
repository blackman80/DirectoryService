using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Departments.ValueObjects;

public record Name
{
    public const int MAX_LENGTH = 150;
    public const int MIN_LENGTH = 3;
    private Name(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < MIN_LENGTH
                                             || value.Length > MAX_LENGTH)
        {
            return Result.Failure<Name>("Invalid name");
        }

        return new Name(value);
    }
}