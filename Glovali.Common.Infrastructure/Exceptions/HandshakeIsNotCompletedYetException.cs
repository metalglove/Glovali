using System;
using Glovali.Common.Exceptions;

namespace Glovali.Common.Infrastructure.Exceptions
{
    /// <summary>
    /// Represents the <see cref="HandshakeIsNotCompletedYetException"/> class.
    /// Thrown when a connecter has not yet completed its handshake.
    /// </summary>
    [Serializable]
    public sealed class HandshakeIsNotCompletedYetException : SerializableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandshakeIsNotCompletedYetException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public HandshakeIsNotCompletedYetException(string message) : base(message)
        {

        }
    }
}
