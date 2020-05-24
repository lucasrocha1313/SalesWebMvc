using System.Collections.Generic;
using System.Threading.Tasks;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> FindAllAsync();
    }
}