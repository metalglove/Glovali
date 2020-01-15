using Glovali.Common.Application.Configurations;
using Glovali.Common.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Glovali.Common.Persistence.EFCore.Infrastructure
{
    /// <summary>
    /// Represents the <see cref="DatabaseFactory{TDbContext}"/> class; an implementation of the abstract <see cref="DesignTimeDbContextFactoryBase{TContext}"/> class.
    /// </summary>
    public abstract class DatabaseFactory<TDbContext> : DesignTimeDbContextFactoryBase<TDbContext>, IFactory<TDbContext> where TDbContext : DbContext
    {
        private readonly string[] _arguments = { "" };

        /// <summary>
        /// Initializes in instance of the <see cref="DatabaseFactory{TDbContext}"/> class.
        /// </summary>
        protected DatabaseFactory() : base(null, null, false)
        {

        }

        /// <summary>
        /// Initializes an instance of the <see cref="DatabaseFactory{TDbContext}"/> class.
        /// </summary>
        /// <param name="dbConfigurationOptions">The options.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="enableSensitiveLogging">The enable sensitive logging flag.</param>
        protected DatabaseFactory(IOptions<DbConfiguration> dbConfigurationOptions, ILoggerFactory loggerFactory, bool enableSensitiveLogging) : base(dbConfigurationOptions, loggerFactory, enableSensitiveLogging)
        {

        }

        /// <inheritdoc cref="IFactory{TResult}.Create"/>
        public TDbContext Create()
        {
            return base.CreateDbContext(_arguments);
        }
    }
}
