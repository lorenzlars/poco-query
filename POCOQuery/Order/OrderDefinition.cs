using System.ComponentModel;

namespace POCOQuery.Order
{
    /// <summary>
    /// This class represents a definition with which can be ordered.
    /// </summary>
    public class OrderDefinition
    {
        /// <summary>
        /// The name of the property with which should ordered.
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// The order direction.
        /// </summary>
        public ListSortDirection Direction { get; set; }
    }
}