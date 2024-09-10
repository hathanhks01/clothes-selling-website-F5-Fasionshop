using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class GioHang
{
    public Guid Id { get; set; }

    public Guid? IdKh { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public string? GhiChu { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<GioHangChiTiet> GioHangChiTiets { get; set; } = new List<GioHangChiTiet>();

    public virtual KhachHang? IdKhNavigation { get; set; }
}
