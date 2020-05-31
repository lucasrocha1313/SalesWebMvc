using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
    public interface ISalesRecordsService
    {
         Task<IEnumerable<SalesRecord>> FindByDateAsync(DateTime minDate, DateTime maxDate);
         Task<IEnumerable<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime minDate, DateTime maxDate);
    }
}