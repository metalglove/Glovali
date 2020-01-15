using System;

namespace Glovali.Common.Messages.Interfaces
{
    /// <summary>
    /// Represents the <see cref="IMessage"/> interface.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets and sets the id.
        /// </summary>
        Guid Id { get; }
    }
}
