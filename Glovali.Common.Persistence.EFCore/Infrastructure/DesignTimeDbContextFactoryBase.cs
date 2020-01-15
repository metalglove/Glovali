using Glovali.Common.Application.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Glovali.Common.Persistence.EFCore.Infrastructure
{
    /// <summary>
    /// Represents the <see cref="DesignTimeDbContextFactoryBase{TContext}"/> class.
    /// </summary>
    /// <typeparam name="TContext">The database context type.</typeparam>
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private DbContextOptionsBuilder<TContext> _dbContextOptionsBuilder;
        private readonly DbConfiguration _dbConfiguration;
        private readonly ILoggerFactory _loggerFactory;
        private readonly bool _enableSensitiveLogging;

        /// <summary>
        /// Initializes an instance of the <see cref="DesignTimeDbContextFactoryBase{TContext}"/> class.
        /// </summary>
        /// <param name="dbConfigurationOptions">The options.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="enableSensitiveLogging">The enable sensitive logging flag.</param>
        protected DesignTimeDbContextFactoryBase(IOptions<DbConfiguration> dbConfigurationOptions, ILoggerFactory loggerFactory, bool enableSensitiveLogging)
        {
            _dbConfiguration = dbConfigurationOptions == null
                ? GetDbConfiguration()
                : dbConfigurationOptions.Value;
            _loggerFactory = loggerFactory;
            _enableSensitiveLogging = enableSensitiveLogging;
        }

        /// <summary>
        /// Creates a new DbContext instance using options.
        /// </summary>
        /// <param name="dbContextOptions">The options.</param>
        /// <returns>Returns a new DbContext instance.</returns>
        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> dbContextOptions);

        /// <summary>
        /// Creates a new DbContext instance using string arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Returns a new DbContext instance.</returns>
        public virtual TContext CreateDbContext(string[] args)
        {
            if (_dbContextOptionsBuilder == null)
                SetDbContextOptionsBuilder(_dbConfiguration.ConnectionString, _dbConfiguration.DbProvider);
            return CreateNewInstance(_dbContextOptionsBuilder.Options);
        }

        private void SetDbContextOptionsBuilder(string connectionString, string providerName)
        {
            // If both empty, continue.
            // The in memory database will be selected.
            if (!string.IsNullOrEmpty(providerName) || !string.IsNullOrEmpty(connectionString))
            {
                if (string.IsNullOrEmpty(connectionString))
                    throw new ArgumentException($"Connection string '{connectionString}' is null or empty.", nameof(connectionString));

                if (string.IsNullOrEmpty(providerName))
                    throw new ArgumentException($"DbProvider string '{providerName}' is null or empty.", nameof(connectionString));
            }

            DbContextOptionsBuilder<TContext> optionsBuilder = new DbContextOptionsBuilder<TContext>();

            // Adds lazy loading.
            if (_dbConfiguration.UseLazyLoading)
                optionsBuilder.UseLazyLoadingProxies();

            // Switches to the correct DbProvider.
            switch (_dbConfiguration.DbProvider.ToLower())
            {
                case "mssql":
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                case "mysql":
                    optionsBuilder.UseMySql(connectionString);
                    break;
                default:
                    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
                    break;
            }

            // Adds logging.
            if (_loggerFactory != null)
            {
                optionsBuilder.UseLoggerFactory(_loggerFactory);
                if (_enableSensitiveLogging)
                    optionsBuilder.EnableSensitiveDataLogging();
            }

            _dbContextOptionsBuilder = optionsBuilder;
        }

        /// <summary>
        /// Gets the database configuration to construct the database connection in design time.
        /// </summary>
        /// <returns></returns>
        protected abstract DbConfiguration GetDbConfiguration();
    }
}
