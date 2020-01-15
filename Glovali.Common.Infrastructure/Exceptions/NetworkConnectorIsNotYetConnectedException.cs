using System;
using Glovali.Common.Exceptions;

namespace Glovali.Common.Infrastructure.Exceptions
{
    /// <summary>
    /// Represents the <see cref="NetworkConnectorIsNotYetConnectedException"/> class.
    /// Thrown when the network connecter is not yet connected.
    /// </summary>
    [Serializable]
    public sealed class NetworkConnectorIsNotYetConnectedException : SerializableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConnectorIsNotYetConnectedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public NetworkConnectorIsNotYetConnectedException(string message): base(message)
        {

        }
    }
}
