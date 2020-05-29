using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task Insert(Seller seller)
        {
            await _context.AddAsync(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(s => s.Department).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var seller = await _context.Seller.FirstOrDefaultAsync(s => s.Id == id);

                if(seller != null)
                {
                    _context.Seller.Remove(seller);
                    await _context.SaveChangesAsync();
                }
            }
            catch(DbUpdateException)
            {
                throw new IntegrityException("Can't delete the seller because he/she has sales");
            }
            
        }

        public async Task UpdateAsync(Seller seller)
        {
            var hasAny = await _context.Seller.AnyAsync(s => s.Id == seller.Id);
            if(!hasAny)
            {
                throw new NotFoundException("Seller " + seller.Name +" not found!");
            }

            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}