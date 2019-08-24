using System;
using System.Linq;
using System.Linq.Expressions;
using POCOQuery.Attributes;
using POCOQuery.Exceptions;

namespace POCOQuery
{
    /// <summary>
    /// This is a class with helper methods for the extension classes.
    /// </summary>
    internal static class ExtensionHelper
    {
        /// <summary>
        /// Check if the given generic types are correct to work with.
        /// </summary>
        /// <typeparam name="TSource">The input parameter for the expressions.</typeparam>
        /// <typeparam name="TTarget">The base type for the expression. As to use the attributes Entity and EntityProperties.</typeparam>
        internal static void TypeCheck<TSource, TTarget>()
        {
            if (Attribute.GetCustomAttributes(typeof(TTarget)).All(x => !(x is Entity)))
            {
                throw new GenericTypeTargetException();
            }

            Entity entity = Attribute.GetCustomAttributes(typeof(TTarget)).First(x => x is Entity) as Entity;

            if (entity != null && entity.Type != typeof(TSource))
            {
                throw new GenericTypeSourceException();
            }
        }

        /// <summary>
        /// Build a expression from a string which representing the expression.
        /// </summary>
        /// <param name="expressionString">The expression string.</param>
        /// <param name="parameter">The parameter for the expression.</param>
        /// <returns>The expression builds from the expression string.</returns>
        internal static Expression BuildExpression(string expressionString, ParameterExpression parameter)
        {
            Expression expression = parameter;
            var expressionPaths = expressionString.Replace($"{parameter.Name}.", "").Split('.');

            foreach (var path in expressionPaths)
            {
                var property = expression.Type.GetProperty(path);

                if (property == null)
                {
                    throw new ExpressionException();
                }

                expression = Expression.Property(expression, property);
            }

            return expression;
        }

        /// <summary>
        /// Get the expression string from the EntityProperty attribute of the binding property.
        /// </summary>
        /// <typeparam name="TTarget">The type which implements the Entity and EntityProperty attributes.</typeparam>
        /// <param name="property">Name of the property.</param>
        /// <returns>The expression string to build a expression.</returns>
        internal static string GetBinding<TTarget>(string property)
        {
            try
            {
                Attribute attribute = Attribute.GetCustomAttributes(typeof(TTarget).GetProperty(property)).First();
                EntityProperty entityProperty = attribute as EntityProperty;

                if (entityProperty == null)
                {
                    throw new PropertyException(property);
                }

                return entityProperty.Binding;
            } catch (Exception e)
            {
                throw new PropertyException(property, e);
            }
        }
    }
}