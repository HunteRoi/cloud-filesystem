using CloudFileSystem.Domain.Abstractions;
using CloudFileSystem.Domain.Entities;
using CloudFileSystem.Domain.Exceptions;
using CloudFileSystem.Infrastructure.Data.Configurations;
using Dawn;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CloudFileSystem.Infrastructure.Data;

/// <summary>
/// The representation of a relational database context for the entire system.
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext"/>
/// <seealso cref="ICloudFileSystemDbContext"/>
internal class CloudFileSystemDbContext : DbContext, ICloudFileSystemDbContext
{
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<CloudFileSystemDbContext> _logger;

    /// <summary>
    /// Gets or sets the documents.
    /// </summary>
    /// <value>The documents.</value>
    public DbSet<Document> Documents { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CloudFileSystemDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="configuration">The configuration.</param>
    public CloudFileSystemDbContext(DbContextOptions<CloudFileSystemDbContext> options, ILogger<CloudFileSystemDbContext> logger)
        : base(options)
    {
        _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
    }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableDetailedErrors();
        base.OnConfiguring(optionsBuilder);
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Creates the entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    /// <exception cref="CreateException&lt;T&gt;"></exception>
    public async Task<Guid> CreateEntityAsync<T>(T entity) where T : Entity
    {
        _logger.LogDebug("{function} for {@entity}", nameof(CreateEntityAsync), entity);

        try
        {
            await Set<T>().AddAsync(entity).ConfigureAwait(false);
            await SaveChangesAsync().ConfigureAwait(false);
            return entity.Id;
        }
        catch (Exception exception)
        {
            throw new CreateException<T>(exception.Message, exception);
        }
    }

    /// <summary>
    /// Deletes the entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <exception cref="System.InvalidOperationException">Unknown entity {id}</exception>
    /// <exception cref="DeleteException&lt;T&gt;"></exception>
    public async Task DeleteEntityAsync<T>(Guid id) where T : Entity
    {
        _logger.LogDebug("{function} on {id}", nameof(DeleteEntityAsync), id);

        try
        {
            T existingEntity = await GetEntityAsync<T>(id);
            if (existingEntity == null)
            {
                throw new InvalidOperationException($"Unknown entity {id}");
            }
            Set<T>().Remove(existingEntity);
            await SaveChangesAsync().ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            throw new DeleteException<T>(exception.Message, exception);
        }
    }

    /// <summary>
    /// Gets the entities asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    public async Task<IEnumerable<T>> GetEntitiesAsync<T>(Expression<Func<T, bool>> expression) where T : Entity
    {
        _logger.LogDebug("{function} based on {@expression}", nameof(GetEntitiesAsync), expression);

        return await this
            .Set<T>()
            .Where(expression)
            .ToListAsync();
    }

    /// <summary>
    /// Gets the entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public Task<T> GetEntityAsync<T>(Guid id) where T : Entity
    {
        _logger.LogDebug("{function} on {id}", nameof(GetEntityAsync), id);

        return Set<T>().SingleOrDefaultAsync(e => e.Id == id);
    }

    /// <summary>
    /// Gets the entity identifier asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    public async Task<Guid> GetEntityIdAsync<T>(Expression<Func<T, bool>> expression) where T : Entity
    {
        _logger.LogDebug("{function} based on {@expression}", nameof(GetEntityIdAsync), expression);

        return await Set<T>()
            .Where(expression)
            .Select(e => e.Id)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">Unknown entity {entity.Id}</exception>
    /// <exception cref="UpdateException&lt;T&gt;"></exception>
    public async Task<Guid> UpdateEntityAsync<T>(T entity) where T : Entity
    {
        _logger.LogDebug("{function} on {@entity}", nameof(UpdateEntityAsync), entity);

        try
        {
            T existingEntity = await GetEntityAsync<T>(entity.Id);
            if (existingEntity == null)
            {
                throw new InvalidOperationException($"Unknown entity {entity.Id}");
            }

            Entry<T>(existingEntity).CurrentValues.SetValues(entity);
            await SaveChangesAsync().ConfigureAwait(false);

            return entity.Id;
        }
        catch (Exception exception)
        {
            throw new UpdateException<T>(exception.Message, exception);
        }
    }
}