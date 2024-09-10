using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class ChucVu
{
    public int Id { get; set; }

    public string? TenChucVu { get; set; }

    public int? LoaiChucVu { get; set; }

    public string? MoTa { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
