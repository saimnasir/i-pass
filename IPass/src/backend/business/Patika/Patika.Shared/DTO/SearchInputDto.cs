using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patika.Shared.DTO
{
    public class SearchInputDto : PaginationInputDto
    {
        public string SearchText { get; set; } 
    }
}
