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
    public class GioHangChiTietRepositories : IGioHangChiTietRepositories
    {
        private readonly DbduAnTnContext _context;
        public GioHangChiTietRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(GioHangChiTiet gioHangChiTiet)
        {
            await _context.GioHangChiTiets.AddAsync(gioHangChiTiet);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idGioHangCT = await GetById(id);
            _context.GioHangChiTiets.Remove(idGioHangCT);
            await _context.SaveChangesAsync();
        }

        public async  Task<List<GioHangChiTiet>> GetAll()
        {
            return await _context.GioHangChiTiets.ToListAsync();
        }

        public async Task<GioHangChiTiet> GetById(Guid id)
        {
            return await _context.GioHangChiTiets.FindAsync(id);
        }

        public async Task Update(GioHangChiTiet gioHangChiTiet)
        {
            _context.Entry(gioHangChiTiet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
