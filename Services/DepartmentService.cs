using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Interfaces;

namespace SalesWebMvc.Services
{
    public class DepartmentService: BaseService, IDepartmentService
    {        
        public DepartmentService(SalesWebMvcContext context): base(context)
        {            
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(d => d.Name).ToListAsync();
        }
    }
}