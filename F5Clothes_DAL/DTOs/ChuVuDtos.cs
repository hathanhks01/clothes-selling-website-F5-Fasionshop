using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class ChuVuDtos
    {
        public int Id { get; set; }

        public string? TenChucVu { get; set; }

        public int? LoaiChucVu { get; set; }

        public string? MoTa { get; set; }

        public int? TrangThai { get; set; }
    }
}
