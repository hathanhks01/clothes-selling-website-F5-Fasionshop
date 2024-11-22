using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.Models.system;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Reponsitories
{
    public class KhachhangRepo : IKhachhangRepo
    {
        private readonly DbduAnTnContext _context;
        public KhachhangRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task DeleteKh(Guid Id)
        {
            var Kh = await GetByKhachHang(Id);
            _context.Remove(Kh);
            await _context.SaveChangesAsync();
        }
        public async Task<KhachHang> GetByMaKhachHang(string maKH)
        {
            return await _context.KhachHangs.FirstOrDefaultAsync(kh => kh.MaKh == maKH);
        }

        public async Task<List<KhachHang>> GetAllKhachHang()
        {
            return await _context.KhachHangs.ToListAsync();
        }

        public async Task<KhachHang> GetByKhachHang(Guid id)
        {
            return await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<KhachHangDtos>> GetKhachHang(ListKhachHangModel valid)
        {
            var query = _context.KhachHangs.AsQueryable();

            if (!string.IsNullOrEmpty(valid.Keyword))
            {
                query = query.Where(n => n.HoVaTenKh.Contains(valid.Keyword));
            }

            if (valid.IsPublic.HasValue)
            {
                query = query.Where(n => n.TrangThai.HasValue == (valid.IsPublic == 1));
            }
            return await query
                .Select(n => new KhachHangDtos
                {
                    Id = n.Id,
                    MaKh = n.MaKh,
                    HoVaTenKh = n.HoVaTenKh,
                    GioiTinh = n.GioiTinh,
                    NgaySinh = n.NgaySinh,
                    TaiKhoan = n.TaiKhoan,
                    MatKhau = n.MatKhau,
                    SoDienThoai = n.SoDienThoai,
                    Email = n.Email,
                    Image = n.Image,
                    MoTa = n.MoTa,
                    TrangThai = n.TrangThai
                })
                .ToListAsync();
        }

        public async Task UpdateKh(KhachHang Kh)
        {
            _context.Entry(Kh).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
