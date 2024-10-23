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
    public class KhachhangRepo: IKhachhangRepo
    {
        private readonly DbduAnTnContext _context;
        public KhachhangRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddKh(KhachHang Kh)
        {
            _context.Add(Kh);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteKh(Guid Id)
        {
            var Kh = await GetByKhachHang(Id);
            _context.Remove(Kh);
            await _context.SaveChangesAsync();
        }

        public async Task<List<KhachHang>> GetAllKhachHang()
        {
            return await _context.KhachHangs.ToListAsync();
        }

        public async Task<KhachHang> GetByKhachHang(Guid id)
        {
            return await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateKh(KhachHang Kh)
        {
            _context.Entry(Kh).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
