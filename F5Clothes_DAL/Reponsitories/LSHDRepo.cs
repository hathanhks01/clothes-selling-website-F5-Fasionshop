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
    public class LSHDRepo: ILSHDRepo
    {
        private readonly DbduAnTnContext _context;
        public LSHDRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddLs(LichSuHoaDon Ls)
        {
            _context.Add(Ls);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLs(Guid Id)
        {
            var Ls = await GetByLichSuHoaDon(Id);
            _context.Remove(Ls);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LichSuHoaDon>> GetAllLichSuHoaDon()
        {
            return await _context.LichSuHoaDons.ToListAsync();
        }

        public async Task<LichSuHoaDon> GetByLichSuHoaDon(Guid id)
        {
            return await _context.LichSuHoaDons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateLs(LichSuHoaDon Ls)
        {
            _context.Entry(Ls).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
