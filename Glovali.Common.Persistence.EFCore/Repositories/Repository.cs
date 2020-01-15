using Glovali.Common.Domain;
using Glovali.Common.Persistence.EFCore.Abstractions;
using Glovali.Common.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Glovali.Common.Persistence.EFCore.Repositories
{
    /// <summary>
    /// Represents the <see cref="Repository{TEntity, TDbContext}"/> class.
    /// The default implementation of the <see cref="RepositoryBase{TEntity,TDbContext}"/> class.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TDbContext">The database context type.</typeparam>
    public sealed class Repository<TEntity, TDbContext> : RepositoryBase<TEntity, TDbContext> where TEntity : class, IEntity where TDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity, TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="entityValidator">The entity validator.</param>
        /// <param name="logger">The logger.</param>
        public Repository(TDbContext dbContext, IEntityValidator<TEntity> entityValidator, ILogger<Repository<TEntity, TDbContext>> logger) : base(dbContext, entityValidator, logger)
        {

        }
    }
}
