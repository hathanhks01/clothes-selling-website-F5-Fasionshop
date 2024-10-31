using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.Models.system;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
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
        public async Task<bool> ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == id);

            if (khachHang == null)
            {
                return false; // Không tìm thấy khách hàng
            }

            // Kiểm tra mật khẩu cũ
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, khachHang.MatKhau)) // So sánh mật khẩu đã mã hóa
            {
                return false; // Mật khẩu cũ không đúng
            }

            // Mã hóa mật khẩu mới
            khachHang.MatKhau = BCrypt.Net.BCrypt.HashPassword(newPassword);

            // Đặt trạng thái cho đối tượng và lưu thay đổi
            _context.Entry(khachHang).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true; // Đổi mật khẩu thành công
        }

    }
}
