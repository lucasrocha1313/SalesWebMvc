using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index()
        {
            var sellers = await _sellerService.FindAllAsync();
            return View(sellers);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var sellerViewModel = new SellerFormViewModel(){Departments = departments};
            return View(sellerViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if(!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel{Seller = seller, Departments = departments};
                return View(viewModel);
            }
            await _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if(!id.HasValue)
                return RedirectToAction(nameof(Error), new {message = "Id not provided"});

            var seller =  _sellerService.FindByIdAsync(id.Value);

            if(seller == null)
                return RedirectToAction(nameof(Error), new {message = "Id not found"});
            
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if(!id.HasValue)
                return RedirectToAction(nameof(Error), new {message = "Id not provided"});

            var seller =  _sellerService.FindByIdAsync(id.Value);

            if(seller == null)
                return RedirectToAction(nameof(Error), new {message = "Id not found"});
            
            return View(seller);           
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction(nameof(Error), new {message = "Id not provided"});
            }

            var seller = await _sellerService.FindByIdAsync(id.Value);
            if(seller == null)
                return RedirectToAction(nameof(Error), new {message = "Id not found"});
            
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel(){Seller = seller, Departments = departments};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if(!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel{Seller = seller, Departments = departments};
                return View(viewModel);
            }
            if(id != seller.Id)
                return RedirectToAction(nameof(Error), new {message = "Id mismatch"});
            
            try
            {
                await _sellerService.UpdateAsync(seller);
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