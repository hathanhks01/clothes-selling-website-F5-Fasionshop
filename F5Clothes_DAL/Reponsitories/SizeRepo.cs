using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Reponsitories
{
    public class SizeRepo: ISizeRepo
    {
        private readonly DbduAnTnContext _context;
        public SizeRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddSz(Size Sz)
        {
            _context.Add(Sz);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSz(Guid Id)
        {
            var Sz = await GetBySize(Id);
            _context.Remove(Sz);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Size>> GetAllSize()
        {
            return await _context.Sizes.ToListAsync();
        }

        public async Task<Size> GetBySize(Guid id)
        {
            return await _context.Sizes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateSz(Size Sz)
        {
            _context.Entry(Sz).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
