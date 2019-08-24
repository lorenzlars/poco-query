using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using POCOQuery.Attributes;

namespace POCOQuery.Select
{
    /// <summary>
    /// This is a extension of the select method of the <c>IQueryable</c> interface.
    /// </summary>
    public static class SelectExtension
    {
        /// <summary>
        /// Convert the query from <typeparam name="TSource"/> to <typeparam name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The input parameter for the expressions.</typeparam>
        /// <typeparam name="TTarget">The base type for the expression. As to use the attributes Entity and EntityProperties.</typeparam>
        /// <param name="source">The extended type.</param>
        /// <returns>A list of the converted rows from the query.</returns>
        public static List<TTarget> Select<TSource, TTarget>(this IQueryable<TSource> source)
        {
            ExtensionHelper.TypeCheck<TSource, TTarget>();

            List<MemberAssignment> memberInitExpressions = new List<MemberAssignment>();
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "x");
            NewExpression newExpression = Expression.New(typeof(TTarget).GetConstructor(new Type[0]));

            foreach (var property in typeof(TTarget).GetProperties())
            {
                EntityProperty attribute = property.GetCustomAttributes(typeof(EntityProperty), false).First() as EntityProperty;
                MemberExpression memberExpression = (MemberExpression) ExtensionHelper.BuildExpression(attribute.Binding, parameter);
                MemberAssignment binding = Expression.Bind(typeof(TTarget).GetProperty(property.Name), memberExpression);

                memberInitExpressions.Add(binding);
            }

            return source.Select(Expression.Lambda<Func<TSource, TTarget>>(Expression.MemberInit(newExpression, memberInitExpressions), parameter)).ToList();
        }

        /// <summary>
        /// Async convert the query from <typeparam name="TSource"/> to <typeparam name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The input parameter for the expressions.</typeparam>
        /// <typeparam name="TTarget">The base type for the expression. As to use the attributes Entity and EntityProperties.</typeparam>
        /// <param name="source">The extended type.</param>
        /// <returns>A list of the converted rows from the query.</returns>
        public static async Task<List<TTarget>> SelectAsync<TSource, TTarget>(this IQueryable<TSource> source)
        {
            return await Task.Run(() =>
            {
                ExtensionHelper.TypeCheck<TSource, TTarget>();

                List<MemberAssignment> memberInitExpressions = new List<MemberAssignment>();
                ParameterExpression parameter = Expression.Parameter(typeof(TSource), "x");
                NewExpression newExpression = Expression.New(typeof(TTarget).GetConstructor(new Type[0]));

                foreach (var property in typeof(TTarget).GetProperties())
                {
                    EntityProperty attribute = property.GetCustomAttributes(typeof(EntityProperty), false).First() as EntityProperty;
                    MemberExpression memberExpression = (MemberExpression) ExtensionHelper.BuildExpression(attribute.Binding, parameter);
                    MemberAssignment binding = Expression.Bind(typeof(TTarget).GetProperty(property.Name), memberExpression);

                    memberInitExpressions.Add(binding);
                }

                return source.Select(Expression.Lambda<Func<TSource, TTarget>>(Expression.MemberInit(newExpression, memberInitExpressions), parameter)).ToList();
            });
        }
    }
}