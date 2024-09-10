using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class GioHangChiTiet
{
    public Guid Id { get; set; }

    public Guid? IdGh { get; set; }

    public Guid? IdSpct { get; set; }

    public int? SoLuong { get; set; }

    public decimal? DonGia { get; set; }

    public decimal? DonGiaKhiGiam { get; set; }

    public decimal? SoTienGiam { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public string? GhiChu { get; set; }

    public int? TrangThai { get; set; }

    public virtual GioHang? IdGhNavigation { get; set; }

    public virtual SanPhamChiTiet? IdSpctNavigation { get; set; }
}
