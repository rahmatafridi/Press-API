using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts
{
    public class PressDotPageListEntityModel<T>
    {
        public ICollection<T> Data { get; set; }

        /// <summary>
        /// Page index
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total count
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Has previous page
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// Has next age
        /// </summary>
        public bool HasNextPage { get; set; }
    }
}
