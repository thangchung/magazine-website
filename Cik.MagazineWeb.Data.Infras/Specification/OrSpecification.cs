// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrSpecification.cs" company="CIK">
//   Copyright by Thang Chung
// </copyright>
// <summary>
//   Defines the OrSpecification type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Cik.MagazineWeb.Data.Infras.Specification
{
    using System.Linq;

    using Cik.MagazineWeb.Data.Infras.Extensions;

    /// <summary>
    /// The or specification.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public class OrSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrSpecification{TEntity}"/> class.
        /// </summary>
        /// <param name="leftSide">
        /// The left side.
        /// </param>
        /// <param name="rightSide">
        /// The right side.
        /// </param>
        public OrSpecification(Specification<TEntity> leftSide, Specification<TEntity> rightSide)
            : base(leftSide, rightSide)
        {
        }

        /// <summary>
        /// The satisfying entity from.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        public override TEntity SatisfyingEntityFrom(IQueryable<TEntity> query)
        {
            return this.SatisfyingEntitiesFrom(query).FirstOrDefault();
        }

        /// <summary>
        /// The satisfying entities from.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public override IQueryable<TEntity> SatisfyingEntitiesFrom(IQueryable<TEntity> query)
        {
            return query.Where(this._leftSide.Predicate.Or(this._rightSide.Predicate));
        }
    }
}
