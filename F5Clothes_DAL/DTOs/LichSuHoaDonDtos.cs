using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class LichSuHoaDonDtos
    {
        public Guid Id { get; set; }

        public Guid? IdHd { get; set; }

        public string? NguoiThaoTac { get; set; }

        public string? GhiChu { get; set; }

        public int? TrangThai { get; set; }

        public DateTime? NgayTao { get; set; }
    }
}
