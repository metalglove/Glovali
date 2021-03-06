﻿using Glovali.Common.Messages.Interfaces;
using System.Threading.Tasks;

namespace Glovali.Common.Application.Interfaces
{
    /// <summary>
    /// Represents the <see cref="IMessageProcessor"/> interface.
    /// </summary>
    public interface IMessageProcessor
    {
        /// <summary>
        /// Processes a message asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="networkConnector">The network connector.</param>
        /// <returns>Returns an awaitable <see cref="Task"/>.</returns>
        Task ProcessMessageAsync(IMessage message, INetworkConnector networkConnector);
    }
}
