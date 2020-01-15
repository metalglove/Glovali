using Glovali.Common.Messages.Interfaces;
using System;

namespace Glovali.Common.Messages.Abstractions
{
    /// <summary>
    /// Represents the <see cref="Response"/> class.
    /// </summary>
    public abstract class Response : IResponseMessage
    {
        /// <inheritdoc cref="IMessage.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="IResponseMessage.DateTime"/>
        public DateTime DateTime { get; set; }

        /// <inheritdoc cref="IResponseMessage.RequestId"/>
        public Guid RequestId { get; set; }

        /// <inheritdoc cref="IResponseMessage.Success"/>
        public bool Success { get; set; }

        /// <summary>
        /// Gets and sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <param name="message">The message.</param>
        /// <param name="success">The success flag.</param>
        protected Response(Guid requestId, string message, bool success)
        {
            Id = Guid.NewGuid();
            RequestId = requestId;
            DateTime = DateTime.UtcNow;
            Message = message;
            Success = success;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        protected Response()
        {
            Id = Guid.NewGuid();
            DateTime = DateTime.UtcNow;
        }
    }
}
