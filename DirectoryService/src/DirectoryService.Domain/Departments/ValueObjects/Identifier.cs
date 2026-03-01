using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Departments.ValueObjects;

public record Identifier
{
    public const int MAX_LENGTH = 150;
    public const int MIN_LENGTH = 3;
    private Identifier(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Identifier> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)
            || value.Length < MIN_LENGTH
            || value.Length > MAX_LENGTH
            || Regex.IsMatch(value, @"^[a-zA-Z-]+$"))
        {
            return Result.Failure<Identifier>("Invalid identifier");
        }

        return new Identifier(value);
    }
}