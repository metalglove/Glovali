using AutoMapper;
using Glovali.Common.Application.Interfaces;
using Glovali.Common.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glovali.Common.Application.Abstractions
{
    /// <summary>
    /// Represents the <see cref="BaseService{TEntity, TDto, TId}"/> class.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TDto">The dto type.</typeparam>
    /// <typeparam name="TId">The dto id type.</typeparam>
    public abstract class BaseService<TEntity, TDto, TId> : IService<TDto, TId> where TDto : class where TEntity : class, IEntity<TId>
    {
        protected readonly IMapper Mapper;
        protected readonly IRepository<TEntity, TId> EntityRepository;

        /// <summary>
        /// Initializes an instance of the <see cref="BaseService{TEntity, TDto, TId}"/> class.
        /// </summary>
        /// <param name="entityRepository">The entity repository.</param>
        /// <param name="mapper">The mapper.</param>
        protected BaseService(IRepository<TEntity, TId> entityRepository, IMapper mapper)
        {
            Mapper = mapper;
            EntityRepository = entityRepository;
        }

        /// <inheritdoc cref="IService{TEntity, TId}.FindSingleOrDefaultAsync(TId)"/>
        public virtual Task<TDto> FindSingleOrDefaultAsync(TId id)
        {
            return EntityRepository.FindSingleOrDefaultAsync(tr => tr.Id.Equals(id))
                .ContinueWith(trainingRoom => Mapper.Map<TDto>(trainingRoom.Result));
        }

        /// <inheritdoc cref="IService{TEntity, TId}.GetAllAsync()"/>
        public virtual Task<IEnumerable<TDto>> GetAllAsync()
        {
            return EntityRepository.GetAllAsync()
                .ContinueWith(entities => Mapper.Map<IEnumerable<TDto>>(entities.Result));
        }

        /// <inheritdoc cref="IService{TEntity, TId}.CountAsync()"/>
        public virtual Task<int> CountAsync()
        {
            return EntityRepository.CountAsync();
        }

        /// <inheritdoc cref="IService{TEntity, TId}.GetPaginationAsync(int, int)"/>
        public virtual Task<IEnumerable<TDto>> GetPaginationAsync(int pageNumber, int pageSize)
        {
            return EntityRepository.GetPaginationAsync(pageNumber, pageSize)
                .ContinueWith(entities => Mapper.Map<IEnumerable<TDto>>(entities.Result));
        }

        /// <inheritdoc cref="IService{TEntity, TId}.DeleteAsync(TEntity)"/>
        public virtual async Task<(bool success, bool found)> DeleteAsync(TDto dto)
        {
            TEntity entity = Mapper.Map<TEntity>(dto);
            bool found = await EntityRepository.ExistsAsync(u => u.Id.Equals(entity.Id));
            return !found ? (false, false) : (await EntityRepository.DeleteAsync(entity), true);
        }

        /// <inheritdoc cref="IService{TEntity, TId}.UpdateAsync(TEntity)"/>
        public virtual Task<(bool success, TId id, bool updated)> UpdateAsync(TDto dto)
        {
            TEntity entity = Mapper.Map<TEntity>(dto);
            return EntityRepository.UpdateAsync(entity);
        }

        /// <inheritdoc cref="IService{TEntity, TId}.CreateAsync(TEntity)"/>
        public virtual Task<(bool success, TId id)> CreateAsync(TDto dto)
        {
            TEntity entity = Mapper.Map<TEntity>(dto);
            return EntityRepository.CreateAsync(entity);
        }
    }
}
