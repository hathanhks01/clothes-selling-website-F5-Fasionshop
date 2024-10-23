using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class HinhThucThanhToanDtos
    {
        public Guid Id { get; set; }

        public Guid? IdHd { get; set; }

        public Guid? IdKh { get; set; }

        public Guid? IdNv { get; set; }

        public string? MaGiaoDich { get; set; }

        public DateTime? NgayThanhToan { get; set; }

        public decimal? SoTienTra { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public string? GhiChu { get; set; }

        public int? TrangThai { get; set; }

        public int? HinhThucThanhToan1 { get; set; }
    }
}
