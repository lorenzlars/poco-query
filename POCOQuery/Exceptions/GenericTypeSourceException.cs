using System;

namespace POCOQuery.Exceptions
{
    /// <summary>
    /// The target is not implementing <seealso cref="Entity"/>.
    /// </summary>
    public class GenericTypeSourceException : Exception
    {
        public GenericTypeSourceException() : base("The generic type TTarget has to use the Entity attribute.") { }
    }
}