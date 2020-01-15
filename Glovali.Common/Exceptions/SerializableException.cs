using System;
using System.Runtime.Serialization;

namespace Glovali.Common.Exceptions
{
    /// <summary>
    /// Represents the <see cref="SerializableException"/> class.
    /// </summary>
    [Serializable]
    public abstract class SerializableException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableException"/> class.
        /// </summary>
        protected SerializableException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        protected SerializableException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        protected SerializableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatingEntityFailedException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected SerializableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
