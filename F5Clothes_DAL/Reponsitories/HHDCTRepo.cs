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
    public class HDCTRepo: IHDCTRepo
    {
        private readonly DbduAnTnContext _context;
        public HDCTRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddHDCT(HoaDonChiTiet HDCT)
        {
            _context.Add(HDCT);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHDCT(Guid Id)
        {
            var HDCT = await GetByHoaDonChiTiet(Id);
            _context.Remove(HDCT);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HoaDonChiTiet>> GetAllHoaDonChiTiet()
        {
            return await _context.HoaDonChiTiets.ToListAsync();
        }

        public async Task<HoaDonChiTiet> GetByHoaDonChiTiet(Guid id)
        {
            return await _context.HoaDonChiTiets.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateHDCT(HoaDonChiTiet HDCT)
        {
            _context.Entry(HDCT).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
