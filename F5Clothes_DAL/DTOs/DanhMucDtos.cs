using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class DanhMucDtos
    {
        public Guid Id { get; set; }

        public string? TenDanhMuc { get; set; }

        public string? MoTa { get; set; }

        public int? TrangThai { get; set; }

    }
}
