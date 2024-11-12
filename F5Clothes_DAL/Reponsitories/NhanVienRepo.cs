using Azure.Core;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace F5Clothes_DAL.Reponsitories
{
    public class NhanVienRepo : INhanVienRepo
    {
        private readonly DbduAnTnContext _context;
        public NhanVienRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        
        public async Task<List<NhanVien>> GetAllNhanVien()
        {
            return await _context.NhanViens.ToListAsync();
        }

        public async Task DeleteNhanVien(Guid Id)
        {
            var sp = await GetByNhanVien(Id);
			if(sp != null)
			{
				_context.NhanViens.Remove(sp);
				await _context.SaveChangesAsync();
			}
        }

        public async Task<NhanVien> GetByNhanVien(Guid Id)
        {
            return await _context.NhanViens.FirstOrDefaultAsync(x => x.Id == Id);
        }

		public async Task AddNhanVien(NhanVien nv)
		{
            await _context.NhanViens.AddAsync(nv);
            await _context.SaveChangesAsync();
		}

		public async Task UpdateNhanVien(NhanVien nv)
		{
			_context.NhanViens.Update(nv);
			await _context.SaveChangesAsync();
		}

        public async Task<List<NhanVienDtos>> GetNhanVien(ListNhanVienModel nhanvien)
        {
            var query = _context.NhanViens.AsQueryable();

            if (!string.IsNullOrEmpty(nhanvien.Keyword))
            {
                query = query.Where(n => n.HoVaTenNv.Contains(nhanvien.Keyword));
            }
            //if (nhanvien.IsPublic.HasValue)
            //{
            //    query = query.Where(n => n.TrangThai.HasValue == (nhanvien.IsPublic == 1));
            //}
            if (!string.IsNullOrEmpty(nhanvien.Keyword))
            {
                query = query.Where(n => n.HoVaTenNv.Contains(nhanvien.Keyword));
            }
            return await query.Select(n => new NhanVienDtos
            {
                Id = n.Id,
                MaNv = n.MaNv,
                HoVaTenNv = n.HoVaTenNv,
                GioiTinh = n.GioiTinh,
                NgaySinh = n.NgaySinh,
                TaiKhoan = n.TaiKhoan,
                MatKhau = n.MatKhau,
                SoDienThoai = n.SoDienThoai,
                Email = n.Email,
                Image = n.Image,
                MoTa = n.MoTa,
                TrangThai = n.TrangThai
            }).ToListAsync();
        }
    }
}
