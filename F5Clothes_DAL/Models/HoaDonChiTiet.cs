using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class HoaDonChiTiet
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

    public virtual HoaDon? IdHdNavigation { get; set; }

    public virtual SanPhamChiTiet? IdSpctNavigation { get; set; }
}
