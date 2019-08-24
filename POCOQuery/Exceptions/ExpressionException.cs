using System;

namespace POCOQuery.Exceptions
{
    /// <summary>
    /// A given expression string can not converted to a expression.
    /// </summary>
    public class ExpressionException : Exception
    {
        public ExpressionException() : base("Wrong expression string") { }
    }
}