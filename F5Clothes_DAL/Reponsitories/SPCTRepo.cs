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
    public class SPCTRepo: ISPCTRepo
    {
        private readonly DbduAnTnContext _context;
        public SPCTRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddSPCT(SanPhamChiTiet SPCT)
        {
            _context.Add(SPCT);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSPCT(Guid Id)
        {
            var SPCT = await GetBySanPhamChiTiet(Id);
            _context.Remove(SPCT);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SanPhamChiTiet>> GetAllSanPhamChiTiet()
        {
            return await _context.SanPhamChiTiets.ToListAsync();
        }

        public async Task<SanPhamChiTiet> GetBySanPhamChiTiet(Guid id)
        {
            return await _context.SanPhamChiTiets.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateSPCT(SanPhamChiTiet SPCT)
        {
            _context.Entry(SPCT).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
