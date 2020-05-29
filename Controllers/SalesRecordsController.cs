using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services.Interfaces;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController: Controller
    {
        private readonly ISalesRecordsService _salesRecordService;

        public SalesRecordsController(ISalesRecordsService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if(!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if(!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var salesRecords = await _salesRecordService.FindByDateAsync(minDate.Value, maxDate.Value);
            return View(salesRecords);
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
        
    }
}