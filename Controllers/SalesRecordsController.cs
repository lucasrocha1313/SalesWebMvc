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
            FillDatesMaxAndMin(ref minDate, ref maxDate);

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var salesRecords = await _salesRecordService.FindByDateAsync(minDate.Value, maxDate.Value);
            return View(salesRecords);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            FillDatesMaxAndMin(ref minDate, ref maxDate);

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var salesRecords = await _salesRecordService.FindByDateGroupingAsync(minDate.Value, maxDate.Value);
            return View(salesRecords);
        }

        private void FillDatesMaxAndMin(ref DateTime? minDate, ref DateTime? maxDate)
        {
            if(!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if(!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
        }        
    }
}