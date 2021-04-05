using CloudFileSystem.Core.V1.Aggregates;
using CloudFileSystem.Core.V1.Data.Interfaces;
using CloudFileSystem.Core.V1.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CloudFileSystem.Core.V1.Data.Repositories
{
    /// <summary>
    /// Main repository class.
    /// </summary>
    /// <seealso cref="CloudFileSystem.Core.V1.Data.Interfaces.IRepository" />
    public class Repository : IRepository
    {
        /// <summary>
        /// The base context
        /// </summary>
        protected readonly DbContext baseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public Repository(DbContext context)
        {
            this.baseContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc />
        public async Task<Guid> CreateEntityAsync<T>(T entity) where T : Entity
        {
            try
            {
                await this.baseContext.Set<T>().AddAsync(entity).ConfigureAwait(false);
                await this.baseContext.SaveChangesAsync().ConfigureAwait(false);
                return entity.Id;
            } 
            catch (Exception exception)
            {
                throw new CreateException<T>(exception.Message, exception);
            }
        }

        /// <inheritdoc />
        public async Task DeleteEntityAsync<T>(Guid id) where T : Entity
        {
            try
            {
                T existingEntity = await this.GetEntityAsync<T>(id);
                if (existingEntity == null)
                {
                    throw new InvalidOperationException($"Unknown entity {id}");
                }
                this.baseContext.Set<T>().Remove(existingEntity);
                await this.baseContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new DeleteException<T>(exception.Message, exception);
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> GetEntitiesAsync<T>(Expression<Func<T, bool>> expression) where T : Entity
        {
            return await this.baseContext
                .Set<T>()
                .Where(expression)
                .ToListAsync();
        }

        /// <inheritdoc />
        public Task<T> GetEntityAsync<T>(Guid id) where T : Entity
        {
            return this.baseContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        /// <inheritdoc />
        public async Task<Guid> GetEntityIdAsync<T>(Expression<Func<T, bool>> expression) where T : Entity
        {
            return await this.baseContext.Set<T>()
                .Where(expression)
                .Select(e => e.Id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Guid> UpdateEntityAsync<T>(T entity) where T : Entity
        {
            try
            {
                T existingEntity = await this.GetEntityAsync<T>(entity.Id);
                if (existingEntity == null)
                {
                    throw new InvalidOperationException($"Unknown entity {entity.Id}");
                }

                this.baseContext.Entry<T>(existingEntity).CurrentValues.SetValues(entity);
                await this.baseContext.SaveChangesAsync().ConfigureAwait(false);
                
                return entity.Id;
            }
            catch (Exception exception)
            {
                throw new UpdateException<T>(exception.Message, exception);
            }
        }
    }
}
