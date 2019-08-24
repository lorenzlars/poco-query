using System;

namespace POCOQuery.Attributes
{
    /// <summary>
    /// Describe the binding between a property and a base type from the <seealso cref="Entity"/> attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityProperty : Attribute
    {
        /// <summary>
        /// A string to build a linq binding between the property and the base type from <seealso cref="Entity"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// [EntityProperty(Binding = "x.users.name")]
        /// </code>
        /// </example>
        public string Binding { get; set; }
    }
}