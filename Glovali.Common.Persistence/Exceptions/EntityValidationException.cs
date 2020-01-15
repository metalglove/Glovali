using Glovali.Common.Exceptions;
using System;

namespace Glovali.Common.Persistence.Exceptions
{
    /// <summary>
    /// The <see cref="EntityValidationException"/> class is used to denote an exception that occured during the validation of an entity.
    /// </summary>
    [Serializable]
    public sealed class EntityValidationException : SerializableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public EntityValidationException(string message) : base(message)
        {
        }
    }
}
