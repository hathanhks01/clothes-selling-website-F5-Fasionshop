using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class GiamGia
{
    public Guid Id { get; set; }

    public string? MaGiamGia { get; set; }

    public string? TenGiamGia { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public DateTime? NgayBatDau { get; set; }

    public DateTime? NgayKetThuc { get; set; }

    public long? GiaTriGiam { get; set; }

    public int? HinhThucGiam { get; set; }

    public string? GhiChu { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
