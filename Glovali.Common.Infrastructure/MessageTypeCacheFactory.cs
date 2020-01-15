using Glovali.Common.Application.Interfaces;
using Glovali.Common.Messages.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Glovali.Common.Infrastructure
{
    /// <summary>
    /// Represents the <see cref="MessageTypeCacheFactory"/> class.
    /// </summary>
    public class MessageTypeCacheFactory : IFactory<IMessageTypeCache, IEnumerable<Type>>
    {
        /// <summary>
        /// Creates a message type cache based on the types passed. It finds message types per message assembly
        /// type passed by looking for each class if the <see cref="IMessage"/> is assignable from the type.
        /// </summary>
        /// <param name="parameter">The list of message assemblies.</param>
        /// <returns>Returns a new message type cache with messages from the passed assemblies.</returns>
        public IMessageTypeCache Create(IEnumerable<Type> parameter)
        {
            ConcurrentDictionary<string, Type> typeCache = new ConcurrentDictionary<string, Type>();
            parameter
                .SelectMany(GetMessageTypes)
                .ToList()
                .ForEach(messageType => typeCache.TryAdd(messageType.FullName, messageType));

            static IEnumerable<Type> GetMessageTypes(Type type) =>
                Assembly.GetAssembly(type).GetTypes()
                    .Where(type2 => typeof(IMessage).IsAssignableFrom(type2));

            return new MessageTypeCache(typeCache);
        }
    }
}
