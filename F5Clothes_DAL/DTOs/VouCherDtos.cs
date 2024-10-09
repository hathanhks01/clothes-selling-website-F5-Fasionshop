using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class VouCherDtos
    {
        public Guid Id { get; set; }

        public string? MaVouCher { get; set; }

        public string? TenVouCher { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public int? SoLuongMa { get; set; }

        public int? SoLuongDung { get; set; }

        public long? GiaTriGiam { get; set; }

        public long? DieuKienToiThieuHoaDon { get; set; }

        public int? HinhThucGiam { get; set; }

        public int? LoaiVouCher { get; set; }

        public string? GhiChu { get; set; }

        public int? TrangThai { get; set; }
    }
}
