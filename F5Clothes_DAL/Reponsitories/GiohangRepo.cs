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
    public class GiohangRepo: IGioHangRepo
    {
        private readonly DbduAnTnContext _context;
        public GiohangRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddGh(GioHang gh)
        {
            _context.Add(gh);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGh(Guid Id)
        {
            var Gh = await GetByGioHang(Id);
            _context.Remove(Gh);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GioHang>> GetAllGioHang()
        {
            return await _context.GioHangs.ToListAsync();
        }

        public async Task<GioHang> GetByGioHang(Guid id)
        {
            return await _context.GioHangs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateGh(GioHang gh)
        {
            _context.Entry(gh).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
