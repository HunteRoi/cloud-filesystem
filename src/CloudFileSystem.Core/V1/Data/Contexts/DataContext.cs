using CloudFileSystem.Core.V1.Aggregates;
using CloudFileSystem.Core.V1.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CloudFileSystem.Core.V1.Data.Contexts;

/// <summary>
/// The generic data context class.
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
public abstract class DataContext : DbContext, IContext
{
    /// <summary>
    /// The default username
    /// </summary>
    private const string DEFAULT_USERNAME = "UNKNOWN";

    /// <summary>
    /// Initializes a new instance of the <see cref="DataContext" /> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    protected DataContext(DbContextOptions options) : base(options)
    {
        // should receive a service that handles the user identity across the app in order to get
        // the usename for DataContext#GetUsername
    }

    /// <summary>
    /// Gets all entries.
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EntityEntry> GetAllEntries() => this.ChangeTracker
        .Entries()
        .Where(e => e.Entity is Entity && (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

    /// <summary>
    /// Gets the username.
    /// </summary>
    /// <returns></returns>
    private string GetUsername() => DataContext.DEFAULT_USERNAME;

    /// <summary>
    /// Sets the user information.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <param name="entity">The entity.</param>
    private void SetUserInformation(EntityState state, Entity entity)
    {
        switch (state)
        {
            case EntityState.Added:
                entity.SetCreateInformation(this.GetUsername());
                break;

            case EntityState.Modified:
            case EntityState.Deleted:
                entity.SetUpdateInformation(this.GetUsername());
                break;
        }
    }

    /// <inheritdoc />
    public new EntityEntry<T> Entry<T>(T entity) where T : Entity => this.Entry(entity);

    /// <inheritdoc />
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (EntityEntry entityEntry in this.GetAllEntries())
        {
            this.SetUserInformation(entityEntry.State, entityEntry.Entity as Entity);
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}