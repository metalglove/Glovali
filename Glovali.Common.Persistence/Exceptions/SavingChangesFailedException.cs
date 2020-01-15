using System;
using Glovali.Common.Exceptions;

namespace Glovali.Common.Persistence.Exceptions
{
    /// <summary>
    /// Represents the <see cref="SavingChangesFailedException"/> used to throw an Exception when saving changes goes wrong.
    /// </summary>
    [Serializable]
    public sealed class SavingChangesFailedException : SerializableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SavingChangesFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public SavingChangesFailedException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
