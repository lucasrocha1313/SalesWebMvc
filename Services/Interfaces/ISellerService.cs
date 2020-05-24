using System.Collections.Generic;
using System.Threading.Tasks;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
    public interface ISellerService
    {
        Task<List<Seller>> FindAllAsync();
        Task Insert(Seller seller);
        Task<Seller> FindByIdAsync(int id);
        Task RemoveAsync(int id);
        Task UpdateAsync(Seller seller);
    }
}