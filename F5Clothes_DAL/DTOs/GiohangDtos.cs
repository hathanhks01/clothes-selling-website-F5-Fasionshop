using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class GiohangDtos
    {
        public Guid Id { get; set; }

        public Guid? IdKh { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public string? GhiChu { get; set; }

        public int? TrangThai { get; set; }

    }
}
