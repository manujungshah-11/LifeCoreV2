namespace LifeV2.Application.Common;

/// <summary>
/// Thrown when a requested resource does not exist. Mapped to HTTP 404 by middleware.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public static NotFoundException For(string entity, Guid id)
        => new($"{entity} with id '{id}' was not found.");
}
