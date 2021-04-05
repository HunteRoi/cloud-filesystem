using CloudFileSystem.Core.V1.Aggregates;
using CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace CloudFileSystem.Infrastructure.V1.Data.Interfaces
{
    /// <summary>
    /// The data context interface.
    /// </summary>
    public interface ICloudFileSystemDataContext
    {
        /// <summary>
        /// Gets or sets the documents.
        /// </summary>
        /// <value>
        /// The documents.
        /// </value>
        DbSet<Document> Documents { get; set; }

        /// <summary>
        /// Entries the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        EntityEntry<T> Entry<T>(T entity) where T : Entity;

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
