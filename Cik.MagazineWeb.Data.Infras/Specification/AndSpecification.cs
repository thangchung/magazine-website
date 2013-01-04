// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AndSpecification.cs" company="CIK">
//   Copyright by Thang Chung
// </copyright>
// <summary>
//   Defines the AndSpecification type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Cik.MagazineWeb.Data.Infras.Specification
{
    using System.Linq;

    using Cik.MagazineWeb.Data.Infras.Extensions;

    /// <summary>
    /// The and specification.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public class AndSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndSpecification{TEntity}"/> class.
        /// </summary>
        /// <param name="leftSide">
        /// The left side.
        /// </param>
        /// <param name="rightSide">
        /// The right side.
        /// </param>
        public AndSpecification(Specification<TEntity> leftSide, Specification<TEntity> rightSide)
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
            return query.Where(this._leftSide.Predicate.And(this._rightSide.Predicate));
        }
    }
}
