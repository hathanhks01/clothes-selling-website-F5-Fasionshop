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
    public class GioHangRepositories : IGioHangRepositories
    {
        private readonly DbduAnTnContext _context;
        public GioHangRepositories(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task Create(GioHang gioHang)
        {
            await _context.GioHangs.AddAsync(gioHang);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idGioHang = await GetById(id);
            _context.GioHangs.Remove(idGioHang);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GioHang>> GetAll()
        {
            return await _context.GioHangs.ToListAsync();
        }

        public async Task<GioHang> GetById(Guid id)
        {
            return await _context.GioHangs.FindAsync(id);
        }

        public async Task Update(GioHang gioHang)
        {
            _context.Entry(gioHang).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
