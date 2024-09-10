using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class MauSac
{
    public Guid Id { get; set; }

    public string? TenMauSac { get; set; }

    public string? MoTa { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<SanPhamChiTiet> SanPhamChiTiets { get; set; } = new List<SanPhamChiTiet>();
}
