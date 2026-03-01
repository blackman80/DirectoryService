using CSharpFunctionalExtensions;
using DirectoryService.Domain.DepartmentPositions;
using DirectoryService.Domain.Departments;
using DirectoryService.Domain.Locations.ValueObjects;

namespace DirectoryService.Domain.Locations;

public class Location
{
    private List<Department> _departments = [];

    private List<DepartmentPosition> _locationPositions;

    private Location(Name name, Address address, Timezone timezone, List<DepartmentPosition> locations)
    {
        Name = name;
        Address = address;
        Timezone = timezone;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
        _locationPositions = locations;
    }

    public Guid Id { get; private set; }

    public Name Name { get; private set; }

    public Address Address { get; private set; }

    public Timezone Timezone { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyList<Department> Departments => _departments;

    public IReadOnlyList<DepartmentPosition> LocationPositions => _locationPositions;

    public Result ChangeLocation(
        Name newName,
        Address newAddress,
        Timezone newTimezone,
        IEnumerable<DepartmentPosition> locationPositionIds)
    {
        Name = newName;
        Address = newAddress;
        Timezone = newTimezone;

        return Result.Success();
    }

    public static Result<Location> Create(
        Name name,
        Address address,
        Timezone timezone,
        IEnumerable<Guid> locationPositionIds)
    {
        var locations = locationPositionIds
            .Select(locationPositionIds => new DepartmentPosition(Guid.NewGuid(), locationPositionIds))
            .ToList();

        return new Location(name, address, timezone, locations);
    }
}