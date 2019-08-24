using System;
using System.Linq;
using System.Linq.Expressions;

namespace POCOQuery.Order
{
    /// <summary>
    /// This is a helper class to enable the possibility to dynamically order a <c>IQueryable</c> based on a generic value.
    /// </summary>
    /// <typeparam name="TSource">The input parameter for the expressions.</typeparam>
    /// <typeparam name="TTarget">The base type for the expression. As to use the attributes Entity and EntityProperties.</typeparam>
    public static class OrderHelper<TSource, TTarget>
    {
        /// <summary>
        /// Order a <c>IQueryable</c> ascending based on a property.
        /// </summary>
        /// <param name="source">The <c>IQueryable to order.</c></param>
        /// <param name="property">The name of the property with which to order.</param>
        /// <param name="key">A value of the type from the given property.</param>
        /// <returns>A ascending ordered <c>IQueryable</c></returns>
        internal static IQueryable<TSource> Order<TKey>(IQueryable<TSource> source, string property, TKey key)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "x");

            return source.OrderBy(Expression.Lambda<Func<TSource, TKey>>(ExtensionHelper.BuildExpression(ExtensionHelper.GetBinding<TTarget>(property), parameter), parameter));
        }

        /// <summary>
        /// Order a <c>IQueryable</c> descending based on a property.
        /// </summary>
        /// <param name="source">The <c>IQueryable to order.</c></param>
        /// <param name="property">The name of the property with which to order.</param>
        /// <param name="key">A value of the type from the given property.</param>
        /// <returns>A ascending ordered <c>IQueryable</c></returns>
        internal static IQueryable<TSource> OrderDescending<TKey>(IQueryable<TSource> source, string property, TKey key)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "x");

            return source.OrderByDescending(Expression.Lambda<Func<TSource, TKey>>(ExtensionHelper.BuildExpression(ExtensionHelper.GetBinding<TTarget>(property), parameter),
                parameter));
        }
    }
}