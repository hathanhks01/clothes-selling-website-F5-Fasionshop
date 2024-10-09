using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class SizeDtos
    {
        public Guid Id { get; set; }

        public string? TenSize { get; set; }

        public string? MoTa { get; set; }

        public int? TrangThai { get; set; }
    }
}
