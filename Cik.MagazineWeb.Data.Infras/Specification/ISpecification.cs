// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISpecification.cs" company="CIK">
//   Copyright by Thang Chung
// </copyright>
// <summary>
//   Defines the AndSpecification type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Cik.MagazineWeb.Data.Infras.Specification
{
    using System.Linq;

    /// <summary>
    /// In simple terms, a specification is a small piece of logic which is independent and give an answer 
    /// to the question “does this match ?”. With Specification, we isolate the logic that do the selection 
    /// into a reusable business component that can be passed around easily from the entity we are selecting.
    /// </summary>
    /// <see cref="http://en.wikipedia.org/wiki/Specification_pattern"/>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISpecification<TEntity>
    {
        /// <summary>
        /// The satisfying entity from.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        TEntity SatisfyingEntityFrom(IQueryable<TEntity> query);

        /// <summary>
        /// The satisfying entities from.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        IQueryable<TEntity> SatisfyingEntitiesFrom(IQueryable<TEntity> query);
    }
}
