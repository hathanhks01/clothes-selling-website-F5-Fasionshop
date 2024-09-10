using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class VouCher
{
    public Guid Id { get; set; }

    public string? MaVouCher { get; set; }

    public string? TenVouCher { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayBatDau { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public DateTime? NgayKetThuc { get; set; }

    public int? SoLuongMa { get; set; }

    public int? SoLuongDung { get; set; }

    public long? GiaTriGiam { get; set; }

    public long? DieuKienToiThieuHoaDon { get; set; }

    public int? HinhThucGiam { get; set; }

    public int? LoaiVouCher { get; set; }

    public string? GhiChu { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}
