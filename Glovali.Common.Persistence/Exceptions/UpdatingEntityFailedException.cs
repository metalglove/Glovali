using System;
using Glovali.Common.Exceptions;

namespace Glovali.Common.Persistence.Exceptions
{
    /// <summary>
    /// Represents the <see cref="UpdatingEntityFailedException"/> used to throw an Exception when updating an Entity goes wrong.
    /// </summary>
    [Serializable]
    public sealed class UpdatingEntityFailedException : SerializableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatingEntityFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public UpdatingEntityFailedException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
