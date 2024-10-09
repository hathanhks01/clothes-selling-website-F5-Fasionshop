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
    public class GHCTRepo: IGiohangChiTietRepo
    {
        private readonly DbduAnTnContext _context;
        public GHCTRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddGhct(GioHangChiTiet Ghct)
        {
            _context.Add(Ghct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGhct(Guid Id)
        {
            var Ghct = await GetByGHCT(Id);
            _context.Remove(Ghct);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GioHangChiTiet>> GetAllGHCT()
        {
            return await _context.GioHangChiTiets.ToListAsync();
        }

        public async Task<GioHangChiTiet> GetByGHCT(Guid id)
        {
            return await _context.GioHangChiTiets.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateGhct(GioHangChiTiet Ghct)
        {
            _context.Entry(Ghct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
