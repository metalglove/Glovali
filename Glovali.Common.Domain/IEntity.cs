using System;

namespace Glovali.Common.Domain
{
    /// <summary>
    /// Represents the <see cref="IEntity"/> interface.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets and sets the id.
        /// </summary>
        Guid Id { get; }
    }
}
