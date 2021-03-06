﻿using Glovali.Common.Domain;
using Glovali.Common.Persistence.EFCore.Abstractions;
using Glovali.Common.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Glovali.Common.Persistence.EFCore.Repositories
{
    /// <summary>
    /// Represents the <see cref="Repository{TEntity, TDbContext, TId}"/> class.
    /// The default implementation of the <see cref="RepositoryBase{TEntity, TDbContext, TId}"/> class.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TDbContext">The database context type.</typeparam>
    /// <typeparam name="TId">The entity id type.</typeparam>
    public sealed class Repository<TEntity, TDbContext, TId> : RepositoryBase<TEntity, TDbContext, TId> where TEntity : class, IEntity<TId> where TDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity, TDbContext, TId}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="entityValidator">The entity validator.</param>
        /// <param name="logger">The logger.</param>
        public Repository(TDbContext dbContext, IEntityValidator<TEntity, TId> entityValidator, ILogger<Repository<TEntity, TDbContext, TId>> logger) : base(dbContext, entityValidator, logger)
        {

        }
    }
}
