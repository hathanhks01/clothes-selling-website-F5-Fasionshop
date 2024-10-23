using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class DiaChiDtos
    {
        public Guid Id { get; set; }

        public Guid? IdKh { get; set; }

        public string? DiaChiChiTiet { get; set; }

        public string? PhuongXa { get; set; }

        public string? QuanHuyen { get; set; }

        public string? TinhThanh { get; set; }

        public string? QuocGia { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public string? GhiChu { get; set; }

        public int? TrangThai { get; set; }

    }
}
