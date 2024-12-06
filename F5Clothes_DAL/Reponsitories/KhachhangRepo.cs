
﻿using F5Clothes_DAL.DTOs;
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

        public async Task<List<KhachHang>> GetAllKhachHang()
        {
            return await _context.KhachHangs.ToListAsync();
        }

        public async Task<KhachHang> GetByKhachHang(Guid id)
        {
            return await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<KhachHang> GetByMaKhachHang(string maKH)
        {
            return await _context.KhachHangs.FirstOrDefaultAsync(kh => kh.MaKh == maKH);
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
        public async Task<bool> UpdateProfile(Guid id, KhachHangProfileUpdateDto profileUpdateDto)
        {
            var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == id);

            if (khachHang == null)
            {
                return false; // Customer not found
            }

            // Update only non-null fields from the DTO
            if (!string.IsNullOrEmpty(profileUpdateDto.HoVaTenKh))
            {
                khachHang.HoVaTenKh = profileUpdateDto.HoVaTenKh;
            }

            if (profileUpdateDto.GioiTinh.HasValue)
            {
                khachHang.GioiTinh = profileUpdateDto.GioiTinh.Value;
            }

            if (profileUpdateDto.NgaySinh.HasValue)
            {
                khachHang.NgaySinh = profileUpdateDto.NgaySinh.Value;
            }

            if (!string.IsNullOrEmpty(profileUpdateDto.SoDienThoai))
            {
                khachHang.SoDienThoai = profileUpdateDto.SoDienThoai;
            }

            if (!string.IsNullOrEmpty(profileUpdateDto.Email))
            {
                khachHang.Email = profileUpdateDto.Email;
            }

            if (!string.IsNullOrEmpty(profileUpdateDto.Image))
            {
                khachHang.Image = profileUpdateDto.Image;
            }

            // Set the entity state as modified and save changes
            _context.Entry(khachHang).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true; // Profile updated successfully
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

        public async Task<KhachHang> GetByTK(string username)
        {
            return await _context.KhachHangs.FirstOrDefaultAsync(x => x.TaiKhoan == username);
        }
    }
}
