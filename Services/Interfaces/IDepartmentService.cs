using System.Collections.Generic;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
    public interface IDepartmentService
    {
        List<Department> FindAll();
    }
}