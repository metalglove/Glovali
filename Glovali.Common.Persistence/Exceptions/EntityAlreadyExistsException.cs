using System;
using Glovali.Common.Exceptions;

namespace Glovali.Common.Persistence.Exceptions
{
    /// <summary>
    /// Represents the <see cref="EntityAlreadyExistsException"/> used to throw an Exception when a predicate returns with a duplicate exception.
    /// </summary>
    [Serializable]
    public sealed class EntityAlreadyExistsException : SerializableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public EntityAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
