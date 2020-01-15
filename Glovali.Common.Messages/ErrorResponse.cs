using Glovali.Common.Messages.Abstractions;

namespace Glovali.Common.Messages
{
    /// <summary>
    /// Represents the <see cref="ErrorResponse"/> class,
    /// </summary>
    public class ErrorResponse : Response
    {
        /// <summary>
        /// Gets and sets the request name.
        /// </summary>
        public string RequestName { get; set; }

        /// <summary>
        /// Gets and sets the response name.
        /// </summary>
        public string ResponseName { get; set; }
    }
}
