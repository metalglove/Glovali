using Glovali.Common.Messages.Interfaces;
using System;

namespace Glovali.Common.Messages.Abstractions
{
    /// <summary>
    /// Represents the <see cref="Request"/> class.
    /// </summary>
    public abstract class Request : IMessage
    {
        /// <inheritdoc cref="IMessage.Id"/>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets and sets the date time.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        protected Request()
        {
            Id = Guid.NewGuid();
            DateTime = DateTime.UtcNow;
        }
    }
}
