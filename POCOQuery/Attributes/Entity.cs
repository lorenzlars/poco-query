using System;

namespace POCOQuery.Attributes
{
    /// <summary>
    /// Use this attribute to make a POCO class as <c>IQueryable</c> target.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class Entity : Attribute
    {
        /// <summary>
        /// The base type from which the <c>IQueryable</c> loads the data.
        /// </summary>
        public Type Type { get; set; }
    }
}