using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class SanPhamDtos
    {
        

        public Guid? IdDm { get; set; }

        public Guid? IdTh { get; set; }

        public Guid? IdXx { get; set; }

        public Guid? IdCl { get; set; }

        public Guid? IdGg { get; set; }

        public string? MaSp { get; set; }

        public string? TenSp { get; set; }

        public int? TheLoai { get; set; }

        public string? ImageDefaul { get; set; }

        public decimal? GiaNhap { get; set; }

        public decimal? GiaBan { get; set; }

        public decimal? DonGiaKhiGiam { get; set; }

        public DateTime? NgayThem { get; set; }

        public DateTime? NgayThemGiamGia { get; set; }

        public string? MoTa { get; set; }

        public int? TrangThai { get; set; }
    }
}
