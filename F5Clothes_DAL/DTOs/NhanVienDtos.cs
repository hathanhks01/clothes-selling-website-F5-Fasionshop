using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class NhanVienDtos
    {
		public int? IdCv { get; set; }
		public string? MaNv { get; set; }
		public string? HoVaTenNv { get; set; }
		public bool? GioiTinh { get; set; }
		public DateOnly? NgaySinh { get; set; }
		public string? TaiKhoan { get; set; }
		public string? MatKhau { get; set; }
		public string? SoDienThoai { get; set; }
		public string? Email { get; set; }
		public string? Image { get; set; }
		public string? DiaChi { get; set; }
		public string? MoTa { get; set; }
		public int? TrangThai { get; set; }
	}
}
