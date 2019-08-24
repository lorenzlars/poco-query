namespace POCOQuery.Filter
{
    /// <summary>
    /// Comparison operators with which can be filtered.
    /// </summary>
    public enum Operator
    {
        Equal,
        NotEqual,
        Greater,
        GreaterOrEqual,
        Less,
        LessOrEqual,
        Contains,
        NotContains,
        StartsWith,
        EndsWith,
        IsNull,
        IsNotNull
    }
}