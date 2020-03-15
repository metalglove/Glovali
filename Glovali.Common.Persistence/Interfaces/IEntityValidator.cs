using Glovali.Common.Domain;

namespace Glovali.Common.Persistence.Interfaces
{
    /// <summary>
    /// Represents the <see cref="IEntityValidator{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The Entity to validate.</typeparam>
    public interface IEntityValidator<in T, in TId> where T : IEntity<TId>
    {
        /// <summary>
        /// Validates the provided Entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns <c>true</c> if the Entity is validate; otherwise, <c>false</c>.</returns>
        bool Validate(T entity);
    }
}
