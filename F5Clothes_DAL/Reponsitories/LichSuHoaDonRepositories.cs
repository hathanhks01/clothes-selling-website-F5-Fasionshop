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
    public class LichSuHoaDonRepositories : ILichSuHoaDonRepositories
    {
        private readonly DbduAnTnContext _context;
        public LichSuHoaDonRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(LichSuHoaDon lichSuHoaDon)
        {
            await _context.LichSuHoaDons.AddAsync(lichSuHoaDon);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idLichSuHoaDon = await GetById(id);
            _context.LichSuHoaDons.Remove(idLichSuHoaDon);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LichSuHoaDon>> GetAll()
        {
            return await _context.LichSuHoaDons.ToListAsync();
        }

        public async Task<LichSuHoaDon> GetById(Guid id)
        {
            return await _context.LichSuHoaDons.FindAsync(id);
        }

        public async Task Update(LichSuHoaDon lichSuHoaDon)
        {
            _context.Entry(lichSuHoaDon).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
