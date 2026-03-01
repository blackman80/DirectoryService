using CSharpFunctionalExtensions;
using DirectoryService.Domain.DepartmentPositions;
using DirectoryService.Domain.Positions.ValueObjects;

namespace DirectoryService.Domain.Positions;

public class Position
{
    private List<DepartmentPosition> _positions;

    private Position(Name name, Description description, List<DepartmentPosition> positions)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
        _positions = positions;
    }

    public Guid Id { get; private set; }

    public Name Name { get; private set; }

    public Description? Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyList<DepartmentPosition> Positions => _positions;

    public Result ChangePosition(
        long id, Name name, Description description)
    {
        Name = name;
        Description = description;

        return Result.Success();
    }

    public static Result<Position> Create(
        Name name,
        Description description,
        IEnumerable<Guid> positionIds)
    {
        //сделал по видосу из второго интенсива для связи между Position и DepartmentPosition.
        //Вообще неуверен, что это правильно... В классе Location аналогичная ситуация
        var positions = positionIds
            .Select(positionIds => new DepartmentPosition(Guid.NewGuid(), positionIds))
            .ToList();

        return new Position(name, description, positions);
    }
}