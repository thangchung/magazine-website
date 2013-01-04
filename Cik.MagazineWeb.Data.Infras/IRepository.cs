// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="CIK">
//   Copyright by Thang Chung
// </copyright>
// <summary>
//   Defines the SortOrder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Cik.MagazineWeb.Data.Infras
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Cik.MagazineWeb.Data.Infras.Specification;

    /// <summary>
    /// The Repository interface.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets entity by key.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="keyValue">The key value.</param>
        /// <returns></returns>
        TEntity GetByKey<TEntity>(object keyValue) where TEntity : class;

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class;

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary>
        /// Gets one entity based on matching criteria
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        TEntity Single<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        /// <summary>
        /// Gets single entity using specification
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        TEntity Single<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary>
        /// Firsts the specified predicate.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        /// <summary>
        /// Gets first entity with specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        TEntity First<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Attach<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Delete<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Deletes one or many entities matching the specified criteria
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        void Delete<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        /// <summary>
        /// Deletes entities which satify specificatiion
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        void Delete<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary>
        /// Updates changes of the existing entity. 
        /// The caller must later call SaveChanges() on the repository explicitly to save the entity to database
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Find<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        TEntity FindOne<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;

        /// <summary>
        /// Gets the specified order by.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class;

        /// <summary>
        /// Gets the specified criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class;

        /// <summary>
        /// Gets entities which satifies a specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get<TEntity, TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class;

        /// <summary>
        /// Counts the specified entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        int Count<TEntity>() where TEntity : class;

        /// <summary>
        /// Counts entities with the specified criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        int Count<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        /// <summary>
        /// Counts entities satifying specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        int Count<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>The unit of work.</value>
        IUnitOfWork UnitOfWork { get; }
    }    
}
