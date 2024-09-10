using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class DiaChi
{
    public Guid Id { get; set; }

    public Guid? IdKh { get; set; }

    public string? DiaChiChiTiet { get; set; }

    public string? PhuongXa { get; set; }

    public string? QuanHuyen { get; set; }

    public string? TinhThanh { get; set; }

    public string? QuocGia { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public string? GhiChu { get; set; }

    public int? TrangThai { get; set; }

    public virtual KhachHang? IdKhNavigation { get; set; }
}
