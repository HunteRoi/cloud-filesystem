using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CloudFileSystem.Core.V1.Aggregates;

/// <summary>
/// Entity class representing anything from the database.
/// </summary>
/// <seealso cref="System.IEquatable{CloudFileSystem.Core.V1.Aggregates.Entity}" />
public abstract class Entity : IEquatable<Entity>
{
    /// <summary>
    /// Gets a value indicating whether this instance has been created.
    /// </summary>
    /// <value><c>true</c> if this instance has been created; otherwise, <c>false</c>.</value>
    [NotMapped]
    private bool HasBeenCreated => string.IsNullOrWhiteSpace(this.CreatedBy);

    /// <summary>
    /// Gets the created at.
    /// </summary>
    /// <value>The created at.</value>
    public virtual DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the created by.
    /// </summary>
    /// <value>The created by.</value>
    public virtual string CreatedBy { get; private set; }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public virtual Guid Id { get; private set; }

    /// <summary>
    /// Gets the last modified at.
    /// </summary>
    /// <value>The last modified at.</value>
    public virtual DateTime? LastModifiedAt { get; private set; }

    /// <summary>
    /// Gets the last modified by.
    /// </summary>
    /// <value>The last modified by.</value>
    public virtual string LastModifiedBy { get; private set; }

    /// <summary>
    /// Gets the row version.
    /// </summary>
    /// <value>The row version.</value>
    public virtual byte[] RowVersion { get; private set; }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// <see langword="true" /> if the current object is equal to the <paramref name="other" />
    /// parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals([AllowNull] Entity other)
    {
        if (other is null) return false;
        return this.Id == other.Id;
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance;
    /// otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object obj)
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

    /// <summary>
    /// Sets the create information.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <exception cref="System.ArgumentException"></exception>
    public void SetCreateInformation(string username)
    {
        if (!HasBeenCreated) throw new ArgumentException("These properties are now readonly!");

        this.CreatedAt = DateTime.UtcNow;
        this.CreatedBy = username ?? throw new ArgumentNullException(nameof(username));
    }

    /// <summary>
    /// Sets the update information.
    /// </summary>
    /// <param name="username">The username.</param>
    public void SetUpdateInformation(string username)
    {
        if (!this.HasBeenCreated) throw new ArgumentException("You need to create the entity before updating it!");

        this.LastModifiedAt = DateTime.UtcNow;
        this.LastModifiedBy = username ?? throw new ArgumentNullException(nameof(username));
    }
}