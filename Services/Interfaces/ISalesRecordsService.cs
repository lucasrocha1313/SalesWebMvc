using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
    public interface ISalesRecordsService
    {
         Task<IEnumerable<SalesRecord>> FindByDateAsync(DateTime minDate, DateTime maxDate);
    }
}