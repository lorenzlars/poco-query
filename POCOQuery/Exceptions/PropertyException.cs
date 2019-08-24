using System;

namespace POCOQuery.Exceptions
{
    /// <summary>
    /// A given property name could not be found.
    /// </summary>
    public class PropertyException : Exception
    {
        public PropertyException(string property) : base($"The property {property} is not valid.") { }
        public PropertyException(string property, Exception exception) : base($"The property {property} is not valid.", exception) { }
    }
}