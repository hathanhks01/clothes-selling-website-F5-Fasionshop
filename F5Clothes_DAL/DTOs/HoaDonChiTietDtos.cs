﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class HoaDonChiTietDtos
    {
        public Guid Id { get; set; }

        public Guid? IdHd { get; set; }

        public Guid? IdSpct { get; set; }

        public int? SoLuong { get; set; }

        public decimal? DonGia { get; set; }

        public decimal? DonGiaKhiGiam { get; set; }

        public DateTime? NgayTao { get; set; }

        public string? GhiChu { get; set; }

        public int? TrangThai { get; set; }

        public DateTime? NgayCapNhat { get; set; }

    }
}
