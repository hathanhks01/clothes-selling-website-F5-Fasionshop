using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.Reponsitories;
using F5Clothes_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.Services
{
	public class NhanVienService : INhanVienService
	{
		private readonly INhanVienRepo _nhanvienRepo;
        public NhanVienService(INhanVienRepo nhanVienRepo)
        {
            _nhanvienRepo = nhanVienRepo;
        }
        public async Task AddNhanVienAsync(NhanVienDtos nvRequest)
		{
			try
			{
				var nhanvien = new NhanVien
				{
					Id = Guid.NewGuid(),
					IdCv = nvRequest.IdCv,
					MaNv = nvRequest.MaNv,
					HoVaTenNv = nvRequest.HoVaTenNv,
					GioiTinh = nvRequest.GioiTinh,
					NgaySinh = nvRequest.NgaySinh,
					TaiKhoan = nvRequest.TaiKhoan,
					MatKhau = nvRequest.MatKhau,
					SoDienThoai = nvRequest.SoDienThoai,
					Email = nvRequest.Email,
					Image = nvRequest.Image,
					DiaChi = nvRequest.DiaChi,
					MoTa = nvRequest.MoTa,
					TrangThai = nvRequest.TrangThai
				};
				await _nhanvienRepo.AddNhanVien(nhanvien);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task DeleteNhanVienAsync(Guid id)
		{
			await _nhanvienRepo.DeleteNhanVien(id);
		}

		public async Task<IEnumerable<NhanVien>> GetAllNhanVienAsync()
		{
			return await _nhanvienRepo.GetAllNhanVien();
		}

		public async Task<NhanVien?> GetNhanVienByIdAsync(Guid id)
		{
			return await _nhanvienRepo.GetByNhanVien(id);
		}

		public Task UpdateNhanVienAsync(NhanVienDtos nhanVien)
		{
			throw new NotImplementedException();
		}
	}
}
