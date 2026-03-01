using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using DirectoryService.Domain.Departments.ValueObjects;
using DirectoryService.Domain.Locations;

namespace DirectoryService.Domain.Departments;

public class Department
{
    private List<Department> _children = [];
    private List<Location> _locations = [];

    private Department(
        Name name,
        Identifier identifier,
        string path,
        short depth,
        Department? parent,
        IEnumerable<Location> locations)
    {
        Id = Guid.NewGuid();
        Name = name;
        Identifier = identifier;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
        Path = path;
        Parent = parent;
        Depth = depth;
        _locations.AddRange(locations);
        IsActive = true;
    }

    public Guid Id { get; private set; }

    public Name Name { get; private set; }

    public Identifier Identifier { get; private set; }

    public Department? Parent { get; private set; }

    public IReadOnlyList<Department> Children => _children;

    public string Path { get; private set; }

    public short Depth { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyList<Location> Locations => _locations;

    public Result Rename(Name newName, Identifier newIdentifier)
    {
        Name = newName;
        Identifier = newIdentifier;

        return Result.Success();
    }

    public static Result<Department> Create(
        Name name,
        Identifier identifier,
        Department? parent,
        IEnumerable<Location>? locations)
    {
        string? parentPath = parent?.Path;

        short depth;
        if (parent?.Depth == null)
        {
            depth = 1;
        }
        else
        {
            depth = parent.Depth;
            depth++;
        }

        if (locations == null || !locations.Any())
        {
            return Result.Failure<Department>("Invalid locations");
        }

        return new Department(name, identifier, $"{parentPath}.{identifier}", depth, parent, locations);
    }
}