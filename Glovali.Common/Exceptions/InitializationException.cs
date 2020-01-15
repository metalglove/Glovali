using System;

namespace Glovali.Common.Exceptions
{
    /// <summary>
    /// Represents the <see cref="InitializationException"/> class.
    /// Thrown when the initialization goes awry.
    /// </summary>
    [Serializable]
    public sealed class InitializationException : SerializableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InitializationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InitializationException(string message) : base(message)
        {

        }
    }
}
