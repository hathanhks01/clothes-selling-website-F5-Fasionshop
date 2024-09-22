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
    public class HinhThucThanhToanRepositories : IHinhThucThanhToanRepositories
    {
        private readonly DbduAnTnContext _context;
        public HinhThucThanhToanRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(HinhThucThanhToan hinhThucThanhToan)
        {
            await _context.HinhThucThanhToans.AddAsync(hinhThucThanhToan);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idHTTToan = await GetById(id);
            _context.HinhThucThanhToans.Remove(idHTTToan);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HinhThucThanhToan>> GetAll()
        {
            return await _context.HinhThucThanhToans.ToListAsync();
        }

        public async Task<HinhThucThanhToan> GetById(Guid id)
        {
            return await _context.HinhThucThanhToans.FindAsync(id);
        }

        public async Task Update(HinhThucThanhToan hinhThucThanhToan)
        {
            _context.Entry(hinhThucThanhToan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    } 
}
