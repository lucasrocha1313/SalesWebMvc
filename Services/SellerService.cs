using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using SalesWebMvc.Services.Interfaces;

namespace SalesWebMvc.Services
{
    public class SellerService : BaseService, ISellerService
    {
        public SellerService(SalesWebMvcContext context): base(context)
        {
            
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(s => s.Department).FirstOrDefault(s => s.Id == id);
        }

        public void Remove(int id)
        {
            var seller = _context.Seller.FirstOrDefault(s => s.Id == id);

            if(seller != null)
            {
                _context.Seller.Remove(seller);
                _context.SaveChanges();
            }
        }

        public void Update(Seller seller)
        {
            
            if(!_context.Seller.Any(s => s.Id == seller.Id))
            {
                throw new NotFoundException("Seller " + seller.Name +" not found!");
            }

            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}