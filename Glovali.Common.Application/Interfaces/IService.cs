using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glovali.Common.Application.Interfaces
{
    /// <summary>
    /// Represents the <see cref="IService{TDto, TId}"/> interface.
    /// </summary>
    /// <typeparam name="TDto">The dto type.</typeparam>
    /// <typeparam name="TId">The dto id type.</typeparam>
    public interface IService<TDto, TId> where TDto : class
    {
        /// <summary>
        /// Finds a single dto by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns the dto by id.</returns>
        Task<TDto> FindSingleOrDefaultAsync(TId id);

        /// <summary>
        /// Gets all records as dtos.
        /// </summary>
        /// <returns>Returns all dtos.</returns>
        Task<IEnumerable<TDto>> GetAllAsync();

        /// <summary>
        /// Counts the records asynchronously.
        /// </summary>
        /// <returns>Returns the number of records.</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Gets paginated records as dtos.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>Returns all paginated dtos.</returns>
        Task<IEnumerable<TDto>> GetPaginationAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Deletes the provided dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <exception cref="EntityNotFoundException">If the provided dto cannot be found.</exception>
        /// <exception cref="DeletingEntityFailedException">If it fails to the save changes.</exception>
        /// <returns>
        /// Returns (<c>true</c>, <c>true</c>) If the dto is successfully deleted or
        /// (<c>false</c>, <c>true</c>) if the dto was found but not deleted
        ///  otherwise, (<c>false</c>, <c>false</c>) if the delete fails and no dto was found.
        /// </returns>
        Task<(bool success, bool found)> DeleteAsync(TDto dto);

        /// <summary>
        /// Updates the provided dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <exception cref="UpdatingEntityFailedException">If it fails to the save changes.</exception>
        /// <returns>
        /// Returns (<c>true</c>, <see cref="TId"/>, <c>false</c>) If the dto is successfully updated or
        /// (<c>true</c>, <see cref="TId"/>, <c>true</c>) when instead of updating it created a new dto, otherwise
        /// (<c>false</c>, <see cref="TId"/>, <c>false</c>) if it fails to update.
        /// </returns>
        Task<(bool success, TId id, bool updated)> UpdateAsync(TDto dto);

        /// <summary>
        /// Creates the provided dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <exception cref="CreatingEntityFailedException">If it fails to the save changes.</exception>
        /// <returns>
        /// Returns (<c>true</c>, <see cref="TId"/>) If the dto is successfully created;
        /// otherwise, (<c>false</c>, <see cref="TId"/>).
        /// </returns>
        Task<(bool success, TId id)> CreateAsync(TDto dto);
    }
}
