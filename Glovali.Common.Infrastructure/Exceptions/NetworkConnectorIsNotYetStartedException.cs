using System;
using Glovali.Common.Exceptions;

namespace Glovali.Common.Infrastructure.Exceptions
{
    /// <summary>
    /// Represents the <see cref="NetworkConnectorIsNotYetStartedException"/> class.
    /// Thrown when the network connecter is not yet started.
    /// </summary>
    [Serializable]
    public sealed class NetworkConnectorIsNotYetStartedException : SerializableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConnectorIsNotYetStartedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public NetworkConnectorIsNotYetStartedException(string message) : base(message)
        {

        }
    }
}
