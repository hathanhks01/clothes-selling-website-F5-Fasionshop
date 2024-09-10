using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class SanPhamChiTiet
{
    public Guid Id { get; set; }

    public Guid? IdSp { get; set; }

    public Guid? IdMs { get; set; }

    public Guid? IdSize { get; set; }

    public int? SoLuongTon { get; set; }

    public string? MoTa { get; set; }

    public string? QrCode { get; set; }

    public int? TrangThai { get; set; }

    public DateTime? NgayTao { get; set; }

    public virtual ICollection<GioHangChiTiet> GioHangChiTiets { get; set; } = new List<GioHangChiTiet>();

    public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();

    public virtual MauSac? IdMsNavigation { get; set; }

    public virtual Size? IdSizeNavigation { get; set; }

    public virtual SanPham? IdSpNavigation { get; set; }
}
