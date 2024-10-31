using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class SanPhamChiTietDtos
    {

        public Guid Id { get; set; }
        public Guid? IdSp { get; set; } // ID của sản phẩm
        public string? TenSp { get; set; }
        public Guid? IdMs { get; set; }
        public Guid? IdSize { get; set; }
        public int? SoLuongTon { get; set; }
        public string? MoTa { get; set; }
        public string? QrCode { get; set; }
        public int? TrangThai { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
