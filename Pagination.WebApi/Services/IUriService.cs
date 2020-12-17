using Pagination.WebApi.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagination.WebApi.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(CustomerPaginationFilter filter, string route);
    }
}
