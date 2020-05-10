using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController: Controller
    {
        public IActionResult Index()
        {
            var list = new List<Department>();
            list.Add(new Department(){Id = 1, Name = "Electronics"});
            list.Add(new Department(){Id = 2, Name = "Sex"});
            return View(list);
        }
    }
}