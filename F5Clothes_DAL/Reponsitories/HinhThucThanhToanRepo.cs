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
    public class HinhThucThanhToanRepo: IHinhThucThanhToanRepo
    {
        private readonly DbduAnTnContext _context;
        public HinhThucThanhToanRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddHTt(HinhThucThanhToan HTt)
        {
            _context.Add(HTt);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHTt(Guid Id)
        {
            var HTt = await GetByHinhThucThanhToan(Id);
            _context.Remove(HTt);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HinhThucThanhToan>> GetAllHinhThucThanhToan()
        {
            return await _context.HinhThucThanhToans.ToListAsync();
        }

        public async Task<HinhThucThanhToan> GetByHinhThucThanhToan(Guid id)
        {
            return await _context.HinhThucThanhToans.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateHTt(HinhThucThanhToan HTt)
        {
            _context.Entry(HTt).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
