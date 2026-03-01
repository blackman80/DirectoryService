using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Locations.ValueObjects;

public class Timezone
{
    private Timezone(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Timezone> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) 
            || Regex.IsMatch(value, @"^[a-zA-Z]+/[a-zA-Z_]+$"))
        {
            return Result.Failure<Timezone>("Incorrect Iana code line");
        }

        if (TimeZoneInfo.TryFindSystemTimeZoneById(value, out TimeZoneInfo? timeZoneInfo) == false)
        {
            return Result.Failure<Timezone>("Incorrect Iana code");
        }

        return new Timezone(value);
    }
}