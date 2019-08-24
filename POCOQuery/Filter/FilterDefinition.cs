namespace POCOQuery.Filter
{
    /// <summary>
    /// This class represents a definition with which can be filtered.
    /// </summary>
    public class FilterDefinition
    {
        /// <summary>
        /// The name of the property with which should filtered.
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// The comparision operator.
        /// </summary>
        public Operator Operator { get; set; }

        /// <summary>
        /// The value to be filtered with.
        /// </summary>
        public object Value { get; set; }
    }
}