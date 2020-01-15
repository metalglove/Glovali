using System;
using Glovali.Common.Exceptions;

namespace Glovali.Common.Persistence.Exceptions
{
    /// <summary>
    /// Represents the <see cref="DeletingEntityFailedException"/> used to throw an Exception when deleting an Entity goes wrong.
    /// </summary>
    [Serializable]
    public sealed class DeletingEntityFailedException : SerializableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeletingEntityFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public DeletingEntityFailedException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
