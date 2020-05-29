using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
    public class SalesRecordsService: BaseService, ISalesRecordsService
    {
        public SalesRecordsService(SalesWebMvcContext context): base(context)
        {
            
        }

        public async Task<IEnumerable<SalesRecord>> FindByDateAsync(DateTime minDate, DateTime maxDate)
        {
            var salesRecordDatabase = from record in _context.SalesRecord select record;
            
            return await salesRecordDatabase
                .Where(s => s.Date >= minDate && s.Date <= maxDate)
                .Include(s => s.Seller)
                .Include(s => s.Seller.Department)
                .OrderByDescending(s => s.Date)
                .ToListAsync();
        }
    }
}