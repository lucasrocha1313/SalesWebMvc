using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services.Exceptions;
using SalesWebMvc.Services.Interfaces;

namespace SalesWebMvc.Controllers
{
    public class SellersController: Controller
    {
        private readonly ISellerService _sellerService;
        private readonly IDepartmentService _departmentService;

        public SellersController(ISellerService sellerService, IDepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var sellers = _sellerService.FindAll();
            return View(sellers);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var sellerViewModel = new SellerFormViewModel(){Departments = departments};
            return View(sellerViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if(!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel{Seller = seller, Departments = departments};
                return View(viewModel);
            }
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if(!id.HasValue)
                return RedirectToAction(nameof(Error), new {message = "Id not provided"});

            var seller =  _sellerService.FindById(id.Value);

            if(seller == null)
                return RedirectToAction(nameof(Error), new {message = "Id not found"});
            
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if(!id.HasValue)
                return RedirectToAction(nameof(Error), new {message = "Id not provided"});

            var seller =  _sellerService.FindById(id.Value);

            if(seller == null)
                return RedirectToAction(nameof(Error), new {message = "Id not found"});
            
            return View(seller);           
        }

        public IActionResult Edit(int? id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction(nameof(Error), new {message = "Id not provided"});
            }

            var seller = _sellerService.FindById(id.Value);
            if(seller == null)
                return RedirectToAction(nameof(Error), new {message = "Id not found"});
            
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel(){Seller = seller, Departments = departments};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if(!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel{Seller = seller, Departments = departments};
                return View(viewModel);
            }
            if(id != seller.Id)
                return RedirectToAction(nameof(Error), new {message = "Id mismatch"});
            
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new {message = ex.Message});
            }
            
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel()
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.Connection.Id
            };

            return View(viewModel);
        }
    }
}