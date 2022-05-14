using CloudFileSystem.Domain.Entities;
using System.Linq.Expressions;

namespace CloudFileSystem.Domain.Abstractions;

/// <summary>
/// The database context abstraction
/// </summary>
public interface ICloudFileSystemDbContext
{
    /// <summary>
    /// Creates the entity asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task<Guid> CreateEntityAsync<T>(T entity) where T : Entity;

    /// <summary>
    /// Deletes the entity asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task DeleteEntityAsync<T>(Guid id) where T : Entity;

    /// <summary>
    /// Gets the entities asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    Task<IEnumerable<T>> GetEntitiesAsync<T>(Expression<Func<T, bool>> expression) where T : Entity;

    /// <summary>
    /// Gets the entity asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<T> GetEntityAsync<T>(Guid id) where T : Entity;

    /// <summary>
    /// Gets the entity identifier asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    Task<Guid> GetEntityIdAsync<T>(Expression<Func<T, bool>> expression) where T : Entity;

    /// <summary>
    /// Updates the entity asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task<Guid> UpdateEntityAsync<T>(T entity) where T : Entity;

    /// <summary>
    /// Saves the changes asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}