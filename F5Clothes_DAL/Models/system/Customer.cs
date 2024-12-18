﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Models.system
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string? MaKh { get; set; }

        public string? HoVaTenKh { get; set; }

        public bool? GioiTinh { get; set; }

        public DateOnly? NgaySinh { get; set; }

        public string? TaiKhoan { get; set; }

        public string? MatKhau { get; set; }

        public string? SoDienThoai { get; set; }

        public string? Email { get; set; }
        public int? TrangThai { get; set; }
    }
}
