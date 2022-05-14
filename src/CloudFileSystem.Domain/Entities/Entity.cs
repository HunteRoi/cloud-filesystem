namespace CloudFileSystem.Domain.Entities;

/// <summary>
/// Entity class representing anything from the database.
/// </summary>
public abstract class Entity : IEquatable<Entity>
{
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public virtual Guid Id { get; init; }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// <see langword="true"/> if the current object is equal to the <paramref name="other"/>
    /// parameter; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(Entity? other)
    {
        if (other is null) return false;
        return this.Id == other.Id;
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/>, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance;
    /// otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        return this.Equals(obj as Entity);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures
    /// like a hash table.
    /// </returns>
    public override int GetHashCode()
    {
        return this.Id.GetHashCode() ^ 31;
    }
}