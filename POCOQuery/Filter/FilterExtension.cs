using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace POCOQuery.Filter
{
    /// <summary>
    /// This adds a extension method to filter with to the <c>IQueryable</c> interface.
    /// </summary>
    public static class FilterExtension
    {
        /// <summary>
        /// Filter a <c>IQueryable</c> by <param name="filterDefinitions"></param>.
        /// </summary>
        /// <typeparam name="TSource">The input parameter for the expressions.</typeparam>
        /// <typeparam name="TTarget">The base type for the expression. As to use the attributes Entity and EntityProperties.</typeparam>
        /// <param name="source">The extended type.</param>
        /// <param name="filterDefinitions">The filter definition with which to filter.</param>
        /// <returns>A filtered <c>IQueryable</c></returns>
        public static IQueryable<TSource> FilterBy<TSource, TTarget>(this IQueryable<TSource> source, IEnumerable<FilterDefinition> filterDefinitions)
        {
            ExtensionHelper.TypeCheck<TSource, TTarget>();

            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "x");
            List<Expression> expressions = new List<Expression>();

            if (filterDefinitions != null)
            {
                foreach (FilterDefinition filterDefinition in filterDefinitions)
                {
                    expressions.Add(BuildFilterExpression(ExtensionHelper.BuildExpression(ExtensionHelper.GetBinding<TTarget>(filterDefinition.Property), parameter),
                        filterDefinition.Value, filterDefinition.Operator));
                }

                if (expressions.Count > 0)
                {
                    Expression fullExpression = expressions[0];

                    for (int i = 1; i < expressions.Count; i++)
                    {
                        fullExpression = Expression.And(fullExpression, expressions[i]);
                    }

                    return source.Where(Expression.Lambda<Func<TSource, bool>>(fullExpression, parameter));
                }
            }

            return source;
        }

        /// <summary>
        /// Build a filter expression based on the <param name="@operator"/>.
        /// </summary>
        /// <param name="left">The expression to compare with the value.</param>
        /// <param name="value">The value to compare with the expression.</param>
        /// <param name="operator">The comparision operator to compare the expression and the value.</param>
        /// <returns>A expression to compare.</returns>
        private static Expression BuildFilterExpression(Expression left, object value, Operator @operator)
        {
            switch (@operator)
            {
                case Operator.Equal:
                    return Expression.Equal(left, Expression.Constant(value));
                case Operator.NotEqual:
                    return Expression.NotEqual(left, Expression.Constant(value));
                case Operator.Greater:
                    return Expression.GreaterThan(left, Expression.Constant(value));
                case Operator.GreaterOrEqual:
                    return Expression.GreaterThanOrEqual(left, Expression.Constant(value));
                case Operator.Less:
                    return Expression.LessThan(left, Expression.Constant(value));
                case Operator.LessOrEqual:
                    return Expression.LessThanOrEqual(left, Expression.Constant(value));
                case Operator.Contains:
                    return Expression.Call(left, "Contains", null, Expression.Constant(value));
                case Operator.NotContains:
                    return Expression.Not(Expression.Call(left, "Contains", null, Expression.Constant(value)));
                case Operator.StartsWith:
                    return Expression.Call(left, "StartsWith", null, Expression.Constant(value));
                case Operator.EndsWith:
                    return Expression.Call(left, "EndsWith", null, Expression.Constant(value));
                case Operator.IsNull:
                    return Expression.Equal(left, Expression.Constant(null));
                case Operator.IsNotNull:
                    return Expression.NotEqual(left, Expression.Constant(null));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}