using Glovali.Common.Application.Interfaces;
using Glovali.Common.Domain;
using Glovali.Common.Persistence.Exceptions;
using Glovali.Common.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Glovali.Common.Persistence.EFCore.Abstractions
{
    /// <summary>
    /// Represents the <see cref="RepositoryBase{TEntity, TDbContext, TId}"/> class.
    /// A base implementation of a Repository pattern using the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> from EntityFramework.
    /// </summary>
    /// <typeparam name="TEntity">The entity the repository pattern is used with.</typeparam>
    /// <typeparam name="TDbContext">The DbContext the repository pattern is used with.</typeparam>
    /// <typeparam name="TId">The entity id type the repository pattern is used with.</typeparam>
    public abstract class RepositoryBase<TEntity, TDbContext, TId> : IRepository<TEntity, TId> where TDbContext : DbContext where TEntity : class, IEntity<TId>
    {
        protected readonly ILogger<RepositoryBase<TEntity, TDbContext, TId>> Logger;
        protected readonly TDbContext DbContext;
        protected readonly IEntityValidator<TEntity, TId> EntityValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TEntity, TDbContext, TId}"/> class.
        /// </summary>
        /// <param name="dbContext">The DbContext.</param>
        /// <param name="entityValidator">The entity validator.</param>
        /// <param name="logger">The logger.</param>
        protected RepositoryBase(TDbContext dbContext, IEntityValidator<TEntity, TId> entityValidator, ILogger<RepositoryBase<TEntity, TDbContext, TId>> logger)
        {
            DbContext = dbContext;
            EntityValidator = entityValidator;
            Logger = logger;
        }

        /// <inheritdoc cref="IRepository{TEntity, TId}.CreateAsync(TEntity)"/>
        public virtual async Task<(bool success, TId id)> CreateAsync(TEntity entity)
        {
            bool saveSuccess = false;
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            try
            {
                EntityValidator.Validate(entity);
                DbContext.Set<TEntity>().Add(entity);
                int saveResult = await DbContext.SaveChangesAsync();
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception ex)
            {
                CreatingEntityFailedException exception = new CreatingEntityFailedException($"The entity of type {typeof(TEntity).Name} could not be created.", ex);
                Logger.Log(LogLevel.Error, exception, exception.Message);
            }
            return (success: saveSuccess, id: entity.Id);
        }

        /// <inheritdoc cref="IRepository{TEntity, TId}.GetPaginationAsync(int, int)"/>
        public virtual async Task<IEnumerable<TEntity>> GetPaginationAsync(int pageNumber, int pageSize)
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            return await DbContext.Set<TEntity>().Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
        }

        /// <inheritdoc cref="IRepository{TEntity}.CountAsync()"/>
        public virtual async Task<int> CountAsync()
        {
            return await DbContext.Set<TEntity>().CountAsync();
        }

        /// <inheritdoc cref="IRepository{TEntity, TId}.DeleteAsync(TEntity)"/>
        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            bool saveSuccess = false;
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            try
            {
                if (!await DbContext.Set<TEntity>().ContainsAsync(entity))
                    throw new EntityNotFoundException($"The entity of type {typeof(TEntity).Name} could not be found.");

                DbContext.Set<TEntity>().Remove(entity);
                int saveResult = await DbContext.SaveChangesAsync();
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception ex)
            {
                DeletingEntityFailedException exception = new DeletingEntityFailedException($"The entity of type {typeof(TEntity).Name} could not be deleted.", ex);
                Logger.Log(LogLevel.Error, exception, exception.Message);
            }
            return saveSuccess;
        }

        /// <inheritdoc cref="IRepository{TEntity, TId}.ExistsAsync(Expression{Func{TEntity, bool}})"/>
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            return await DbContext.Set<TEntity>().AnyAsync(predicate);
        }

        /// <inheritdoc cref="IRepository{TEntity, TId}.ExistsAsync(Expression{Func{TEntity, bool}})"/>
        public async Task<bool> SaveChangesAsync()
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            bool saveSuccess = false;
            try
            {
                int saveResult = await DbContext.SaveChangesAsync();
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception ex)
            {
                SavingChangesFailedException exception = new SavingChangesFailedException("The changes failed to save.", ex);
                Logger.Log(LogLevel.Error, exception, exception.Message);
            }
            return saveSuccess;
        }

        /// <inheritdoc cref="IRepository{TEntity, TId}.FindManyAsync(Expression{Func{TEntity, bool}})"/>
        public virtual async Task<IEnumerable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            return await DbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        /// <inheritdoc cref="IRepository{TEntity, TId}.FindSingleOrDefaultAsync(Expression{Func{TEntity, bool}})"/>
        public virtual async Task<TEntity> FindSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            return await DbContext.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        /// <inheritdoc cref="IRepository{TEntity, TId}.GetAllAsync()"/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        /// <inheritdoc cref="IRepository{TEntity, TId}.UpdateAsync(TEntity)"/>
        public virtual async Task<(bool success, TId id, bool updated)> UpdateAsync(TEntity entity)
        {
            bool saveSuccess = false;
            using EntityLoadLock.Releaser loadLock = EntityLoadLock.Shared.Lock();
            EntityEntry<TEntity> entry = DbContext.Update(entity);
            try
            {
                int saveResult = await DbContext.SaveChangesAsync();
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception ex)
            {
                UpdatingEntityFailedException exception = new UpdatingEntityFailedException($"The entity of type {typeof(TEntity).Name} failed to update.", ex);
                Logger.Log(LogLevel.Error, exception, exception.Message);
            }
            return (success: saveSuccess, id: entity.Id, updated: entry.State == EntityState.Modified);
        }
    }
}
