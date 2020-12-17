using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagination.WebApi.Filter
{
    public class CustomerPaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public CustomerPaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public CustomerPaginationFilter(int pageNumber,int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
