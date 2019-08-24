using System;

namespace POCOQuery.Exceptions
{
    /// <summary>
    /// The target is not implementing <seealso cref="EntityProperty"/>.
    /// </summary>
    public class GenericTypeTargetException : Exception
    {
        public GenericTypeTargetException() : base("The generic type TSource must match the Entity attribute type.") { }
    }
}