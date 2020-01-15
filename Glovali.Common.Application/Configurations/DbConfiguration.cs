﻿namespace Glovali.Common.Application.Configurations
{
    /// <summary>
    /// Represents the <see cref="DbConfiguration"/> class.
    /// </summary>
    public class DbConfiguration
    {
        /// <summary>
        /// Gets and sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets and sets whether the database uses lazy loading.
        /// </summary>
        public bool UseLazyLoading { get; set; }

        /// <summary>
        /// Gets and sets the database provider.
        /// </summary>
        public string DbProvider { get; set; }
    }
}
