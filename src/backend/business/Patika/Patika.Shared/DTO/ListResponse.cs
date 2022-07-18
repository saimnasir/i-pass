using System.Collections.Generic;

namespace Patika.Shared.DTO
{
    public class ListResponse<T> where T : class
    {
        public List<T> Data { get; set; }
        /// <summary>
        /// current page
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// requested item per page
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// number of pages based on "total count / page size"
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// total item count
        /// </summary>
        public int TotalCount { get; set; }

    }
}