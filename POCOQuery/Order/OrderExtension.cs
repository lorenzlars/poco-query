using System;
using System.ComponentModel;
using System.Linq;

namespace POCOQuery.Order
{
    /// <summary>
    /// This is a extension of the order method of the <c>IQueryable</c> interface.
    /// </summary>
    public static class OrderExtension
    {
        /// <summary>
        /// Order a <c>IQueryable</c> by <param name="orderDefinition"></param>.
        /// </summary>
        /// <typeparam name="TSource">The input parameter for the expressions.</typeparam>
        /// <typeparam name="TTarget">The base type for the expression. As to use the attributes Entity and EntityProperties.</typeparam>
        /// <param name="source">The extended type.</param>
        /// <param name="orderDefinition">The order definition with which to order.</param>
        /// <returns>A ordered <c>IQueryable</c></returns>
        public static IQueryable<TSource> OrderBy<TSource, TTarget>(this IQueryable<TSource> source, OrderDefinition orderDefinition)
        {
            ExtensionHelper.TypeCheck<TSource, TTarget>();

            if (orderDefinition != null && orderDefinition.Direction == ListSortDirection.Ascending)
            {
                var defaultType = GetDefaultValue<TTarget>(orderDefinition.Property);

                return OrderHelper<TSource, TTarget>.Order(source, orderDefinition.Property, defaultType);
            }

            if (orderDefinition != null && orderDefinition.Direction == ListSortDirection.Descending)
            {
                var defaultType = GetDefaultValue<TTarget>(orderDefinition.Property);

                return OrderHelper<TSource, TTarget>.OrderDescending(source, orderDefinition.Property, defaultType);
            }

            return source;
        }

        /// <summary>
        /// Get the default value of the given property from <typeparam name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TTarget">The base type for the expression. As to use the attributes Entity and EntityProperties.</typeparam>
        /// <param name="property">The name of the property.</param>
        /// <returns>The default value.</returns>
        private static dynamic GetDefaultValue<TTarget>(string property)
        {
            try
            {
                Type propertyType = typeof(TTarget).GetProperty(property).PropertyType;

                if (propertyType.IsValueType)
                {
                    return Activator.CreateInstance(propertyType);
                }

                if (propertyType == typeof(string))
                {
                    return "";
                }

                return null;
            } catch (Exception e)
            {
                throw new Exception($"The property {property} is not valid.", e);
            }
        }
    }
}