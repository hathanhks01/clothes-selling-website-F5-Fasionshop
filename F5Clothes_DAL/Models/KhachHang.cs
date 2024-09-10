using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class KhachHang
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

    public virtual ICollection<DiaChi> DiaChis { get; set; } = new List<DiaChi>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();

    public virtual ICollection<HinhThucThanhToan> HinhThucThanhToans { get; set; } = new List<HinhThucThanhToan>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}
