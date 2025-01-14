﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class KhachHangDtos
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

        public string? Image { get; set; }

        public string? MoTa { get; set; }

        public int? TrangThai { get; set; }
    }
    public class ListKhachHangModel
    {
        public string? Keyword { get; set; }
        public int? IsPublic { get; set; }
    }

    public class KhachHangProfileUpdateDto
    {
        public string MaKh { get; set; }
        public string? HoVaTenKh { get; set; } // Full Name
        public bool? GioiTinh { get; set; } // Gender (true for male, false for female, null if unknown)
        public DateOnly? NgaySinh { get; set; } // Date of Birth (nullable DateOnly)
        public string? SoDienThoai { get; set; } // Phone Number
        public string? Email { get; set; } // Email
        public string? Image { get; set; } // Profile Image (URL or base64)
    }


}
