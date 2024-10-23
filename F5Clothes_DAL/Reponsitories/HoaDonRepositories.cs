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
    public class HoaDonRepositories : IHoaDonRepositories
    {
        private readonly DbduAnTnContext _context;
        public HoaDonRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(HoaDon hoaDon)
        {
            await _context.HoaDons.AddAsync(hoaDon);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idHoaDon = await GetById(id);
            _context.HoaDons.Remove(idHoaDon);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HoaDon>> GetAll()
        {
            return await _context.HoaDons.ToListAsync();
        }

        public async Task<HoaDon> GetById(Guid id)
        {
            return await _context.HoaDons.FindAsync(id);
        }

        public async Task Update(HoaDon hoaDon)
        {
            _context.Entry(hoaDon).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
