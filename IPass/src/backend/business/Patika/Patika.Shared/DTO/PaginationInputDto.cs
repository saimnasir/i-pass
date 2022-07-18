using Patika.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patika.Shared.DTO
{
    public class PaginationInputDto : DTO
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public SortType SortType { get; set; } = SortType.ASC;
    }
}
