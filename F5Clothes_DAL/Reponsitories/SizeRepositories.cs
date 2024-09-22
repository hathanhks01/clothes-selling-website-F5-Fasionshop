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
    public class SizeRepositories : ISizeRepositories
    {
        private readonly DbduAnTnContext _context;
        public SizeRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(Size size)
        {
            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idSize = await GetById(id);
            _context.Sizes.Remove(idSize);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Size>> GetAll()
        {
            return await _context.Sizes.ToListAsync();
        }

        public async Task<Size> GetById(Guid id)
        {
            return await _context.Sizes.FindAsync(id);
        }

        public async Task Update(Size size)
        {
            _context.Entry(size).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
