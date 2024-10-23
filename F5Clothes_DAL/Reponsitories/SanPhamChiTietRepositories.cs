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
    public class SanPhamChiTietRepositories : ISanPhamChiTietRepositories
    {
        private readonly DbduAnTnContext _context;
        public SanPhamChiTietRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(SanPhamChiTiet chatLieu)
        {
            await _context.SanPhamChiTiets.AddAsync(chatLieu);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idSanPhamCT = await GetById(id);
            _context.SanPhamChiTiets.Remove(idSanPhamCT);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SanPhamChiTiet>> GetAll()
        {
            return await _context.SanPhamChiTiets.ToListAsync();
        }

        public async Task<SanPhamChiTiet> GetById(Guid id)
        {
            return await _context.SanPhamChiTiets.FindAsync(id);
        }

        public async Task Update(SanPhamChiTiet chatLieu)
        {
            _context.Entry(chatLieu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
