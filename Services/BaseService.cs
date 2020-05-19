using SalesWebMvc.Models;
using SalesWebMvc.Services.Interfaces;

namespace SalesWebMvc.Services
{
    public class BaseService
    {
        protected readonly SalesWebMvcContext _context;

        public BaseService(SalesWebMvcContext context)
        {
            _context = context;
        }
        
    }
}