using System.Collections.Generic;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
    public interface ISellerService
    {
        List<Seller> FindAll();
        void Insert(Seller seller);
        Seller FindById(int id);
        void Remove(int id);
    }
}