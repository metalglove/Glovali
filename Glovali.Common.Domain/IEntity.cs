namespace Glovali.Common.Domain
{
    /// <summary>
    /// Represents the <see cref="IEntity{TId}"/> interface.
    /// </summary>
    /// <typeparam name="TId">The entity id type.</typeparam>
    public interface IEntity<TId>
    {
        /// <summary>
        /// Gets and sets the id.
        /// </summary>
        TId Id { get; }
    }
}
