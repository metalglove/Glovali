﻿using Glovali.Common.Messages.Interfaces;
using System;

namespace Glovali.Common.Messages.Abstractions
{
    /// <summary>
    /// Represents the <see cref="Command"/> class.
    /// </summary>
    public abstract class Command : IMessage
    {
        /// <inheritdoc cref="IMessage.Id"/>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets and sets the date time.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        protected Command()
        {
            Id = Guid.NewGuid();
            DateTime = DateTime.UtcNow;
        }
    }
}
