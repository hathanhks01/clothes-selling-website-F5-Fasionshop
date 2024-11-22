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
    public class HoaDonRepo: IHoaDonRepo
    {
        private readonly DbduAnTnContext _context;
        public HoaDonRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddHd(HoaDon Hd)
        {
            _context.Add(Hd);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHd(Guid Id)
        {
            var Hd = await GetByHoaDon(Id);
            _context.Remove(Hd);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HoaDon>> GetAllHoaDon()
        {
            return await _context.HoaDons.ToListAsync();
        }

        public async Task<HoaDon> GetByHoaDon(Guid id)
        {
            return await _context.HoaDons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateHd(HoaDon Hd)
        {
            _context.Entry(Hd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<HoaDon> GetLatestOrderAsync()
        {
            return await _context.HoaDons
                .OrderByDescending(hd => hd.MaHoaDon)
                .FirstOrDefaultAsync();
        }
    }
}
