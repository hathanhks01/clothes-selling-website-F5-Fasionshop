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
    public class HoaDonChiTietRepositories : IHoaDonChiTietRepositories
    {
        private readonly DbduAnTnContext _context;
        public HoaDonChiTietRepositories(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task Create(HoaDonChiTiet hoaDonChiTiet)
        {
            await _context.HoaDonChiTiets.AddAsync(hoaDonChiTiet);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idHoaDonChiTiet = await GetById(id);
            _context.HoaDonChiTiets.Remove(idHoaDonChiTiet);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HoaDonChiTiet>> GetAll()
        {
            return await _context.HoaDonChiTiets.ToListAsync();
        }

        public async Task<HoaDonChiTiet> GetById(Guid id)
        {
            return await _context.HoaDonChiTiets.FindAsync(id);
        }

        public async Task Update(HoaDonChiTiet hoaDonChiTiet)
        {
            _context.Entry(hoaDonChiTiet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
