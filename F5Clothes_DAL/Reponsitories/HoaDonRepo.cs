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
            var Hd = await _context.HoaDons
                .Include(h => h.HoaDonChiTiets)  // Đảm bảo các bản ghi HoaDonChiTiets được tải
                .Include(h => h.HinhThucThanhToans)  // Đảm bảo các bản ghi HinhThucThanhToans được tải
                .Include(h => h.LichSuHoaDons)
                .FirstOrDefaultAsync(h => h.Id == Id);

            if (Hd == null)
            {
                throw new Exception("HoaDon not found");
            }
            _context.LichSuHoaDons.RemoveRange(Hd.LichSuHoaDons);
            // Xóa các bản ghi phụ thuộc trước
            _context.HoaDonChiTiets.RemoveRange(Hd.HoaDonChiTiets);  // Xóa bản ghi HoaDonChiTiet
            _context.HinhThucThanhToans.RemoveRange(Hd.HinhThucThanhToans);  // Xóa bản ghi HinhThucThanhToan

            // Xóa bản ghi HoaDon
            _context.HoaDons.Remove(Hd);

            // Lưu các thay đổi
            await _context.SaveChangesAsync();
        }





        public async Task<List<HoaDon>> GetAllHoaDon()
        {
            return await _context.HoaDons
                         .Include(hd => hd.IdNvNavigation)  
                         .Include(hd => hd.IdKhNavigation)  
                         .ToListAsync();
        }

        public async Task<HoaDon> GetByHoaDon(Guid id)
        {
            return await _context.HoaDons.FindAsync(id);
        }

        public async Task UpdateHd(HoaDon Hd)
        {
            _context.Entry(Hd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<int> CountHdByMaHoaDonPrefix(string prefix)
        {
            return await _context.HoaDons
                                 .Where(hd => hd.MaHoaDon.StartsWith(prefix)) 
                                 .CountAsync(); 
        }

    }
}
