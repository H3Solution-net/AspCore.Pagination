using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pagination.WebApi.Contexts;
using Pagination.WebApi.Filter;
using Pagination.WebApi.Helpers;
using Pagination.WebApi.Models;
using Pagination.WebApi.Services;
using Pagination.WebApi.Wrappers;

namespace Pagination.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IUriService uriService;
                public CustomerController(ApplicationDbContext context, IUriService uriService)
        {
            this.context = context;
            this.uriService = uriService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CustomerPaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new CustomerPaginationFilter(filter.PageNumber, filter.PageSize);
            var query = context.Customers.AsQueryable();
            
            //Search by firstname and email
            if (!string.IsNullOrEmpty(filter.FirstName)) query = query.Where(x => x.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email)) query = query.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));
            
            var pagedData = await query.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize).ToListAsync();
            
            // calculate total records
            var totalRecords = await query.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, uriService,route);
            return Ok(pagedReponse);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await context.Customers.Where(a => a.Id ==id).FirstOrDefaultAsync();
            return Ok(new Response<Customer>(customer));
        }
    }
}