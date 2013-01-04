// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterRebinder.cs" company="CIK">
//   Copyright by Thang Chung
// </copyright>
// <summary>
//   http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Cik.MagazineWeb.Data.Infras.Extensions
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx
    /// </summary>
    public class ParameterRebinder : ExpressionVisitor
    {
        /// <summary>
        /// The map.
        /// </summary>
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterRebinder"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary>
        /// The replace parameters.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="exp">
        /// The exp.
        /// </param>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        /// <summary>
        /// The visit parameter.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (this.map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}
