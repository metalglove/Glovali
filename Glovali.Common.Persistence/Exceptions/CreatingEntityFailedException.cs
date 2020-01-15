using System;
using Glovali.Common.Exceptions;

namespace Glovali.Common.Persistence.Exceptions
{
    /// <summary>
    /// Represents the <see cref="CreatingEntityFailedException"/> class used to throw an Exception when creating an Entity goes wrong.
    /// </summary>
    [Serializable]
    public sealed class CreatingEntityFailedException : SerializableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatingEntityFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public CreatingEntityFailedException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
