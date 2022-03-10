using CloudFileSystem.Core.V1.Aggregates;
using System.Linq.Expressions;

namespace CloudFileSystem.Core.V1.Data.Interfaces;

/// <summary>
/// Repository interface.
/// </summary>
public interface IRepository
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
    /// Gets entities asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
}