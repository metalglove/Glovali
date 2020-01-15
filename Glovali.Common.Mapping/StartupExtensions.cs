using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Glovali.Common.Mapping
{
    /// <summary>
    /// Represents the <see cref="StartupExtensions"/> class.
    /// </summary>
    public static class StartupExtensions
    {
        /// <summary>
        /// Adds the AutoMapper mappings from the provided assembly.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assembly">The assembly.</param>
        /// <returns>Returns the service collection to chain further upon.</returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection serviceCollection, Assembly assembly)
        {
            serviceCollection.AddAutoMapper(new[] { assembly });
            return serviceCollection;
        }

        /// <summary>
        /// Converts an <see cref="IServiceProvider"/> to an <see cref="IGenericServiceProvider"/>.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>Returns a <see cref="IGenericServiceProvider"/> implementation.</returns>
        public static IGenericServiceProvider ToGenericServiceProvider(this IServiceProvider serviceProvider)
        {
            return new GenericServiceProvider(serviceProvider);
        }
    }
}
