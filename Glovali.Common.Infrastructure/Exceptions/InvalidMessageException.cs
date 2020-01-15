using System;
using Glovali.Common.Exceptions;

namespace Glovali.Common.Infrastructure.Exceptions
{
    /// <summary>
    /// Represents the <see cref="InvalidMessageException"/> used to throw an Exception when an invalid message is encountered.
    /// </summary>
    [Serializable]
    public sealed class InvalidMessageException : SerializableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidMessageException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InvalidMessageException(string message) : base(message)
        {

        }
    }
}
