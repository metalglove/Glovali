using System;
using Glovali.Common.Exceptions;

namespace Glovali.Common.Persistence.Exceptions
{
    /// <summary>
    /// Represents the <see cref="EntityNotFoundException"/> used to throw an Exception when the predicate returns without results.
    /// </summary>
    [Serializable]
    public sealed class EntityNotFoundException : SerializableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public EntityNotFoundException(string message) : base(message)
        {

        }
    }
}
