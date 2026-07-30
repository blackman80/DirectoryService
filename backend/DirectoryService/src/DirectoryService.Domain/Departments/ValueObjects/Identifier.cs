using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Departments.ValueObjects;

public record Identifier //это можно удалять раз есть Slug?
{
    public const int MaxLength = 150;
    public const int MinLength = 3;
    private const string Pattern = "^[a-zA-Z-]+$";

     private static readonly Regex IdentifierRegex = new Regex(
        Pattern,
        RegexOptions.Compiled,
        TimeSpan.FromSeconds(1)
    );
    private Identifier(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Identifier> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)
            || value.Length < MinLength
            || value.Length > MaxLength
            || !IdentifierRegex.IsMatch(value))
            return Result.Failure<Identifier>("Invalid Identifier");

        return new Identifier(value);
    }
    
}