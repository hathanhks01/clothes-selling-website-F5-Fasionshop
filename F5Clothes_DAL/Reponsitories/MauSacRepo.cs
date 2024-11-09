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
    public class MauSacRepo: IMauSacRepo
    {
        private readonly DbduAnTnContext _context;
        public MauSacRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddMs(MauSac Ms)
        {
            _context.Add(Ms);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMs(Guid Id)
        {
            var Ms = await GetByMauSac(Id);
            _context.Remove(Ms);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MauSac>> GetAllMauSac()
        {
            return await _context.MauSacs.ToListAsync();
        }

        public async Task<MauSac> GetByMauSac(Guid id)
        {
            return await _context.MauSacs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateMs(MauSac Ms)
        {
            _context.Entry(Ms).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
