using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class NhanVien
{
    public Guid Id { get; set; }

    public int? IdCv { get; set; }

    public string? MaNv { get; set; }

    public string? HoVaTenNv { get; set; }

    public bool? GioiTinh { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? TaiKhoan { get; set; }

    public string? MatKhau { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public string? Image { get; set; }

    public string? DiaChi { get; set; }

    public string? MoTa { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<HinhThucThanhToan> HinhThucThanhToans { get; set; } = new List<HinhThucThanhToan>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual ChucVu? IdCvNavigation { get; set; }

    public virtual ICollection<RefeshToken> RefeshTokens { get; set; } = new List<RefeshToken>();
}
