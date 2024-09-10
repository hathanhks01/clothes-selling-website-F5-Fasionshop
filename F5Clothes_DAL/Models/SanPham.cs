using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class SanPham
{
    public Guid Id { get; set; }

    public Guid? IdDm { get; set; }

    public Guid? IdTh { get; set; }

    public Guid? IdXx { get; set; }

    public Guid? IdCl { get; set; }

    public Guid? IdGg { get; set; }

    public string? MaSp { get; set; }

    public string? TenSp { get; set; }

    public int? TheLoai { get; set; }

    public string? ImageDefaul { get; set; }

    public decimal? GiaNhap { get; set; }

    public decimal? GiaBan { get; set; }

    public decimal? DonGiaKhiGiam { get; set; }

    public DateTime? NgayThem { get; set; }

    public DateTime? NgayThemGiamGia { get; set; }

    public string? MoTa { get; set; }

    public int? TrangThai { get; set; }

    public virtual ChatLieu? IdClNavigation { get; set; }

    public virtual DanhMuc? IdDmNavigation { get; set; }

    public virtual GiamGium? IdGgNavigation { get; set; }

    public virtual ThuongHieu? IdThNavigation { get; set; }

    public virtual XuatXu? IdXxNavigation { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<SanPhamChiTiet> SanPhamChiTiets { get; set; } = new List<SanPhamChiTiet>();
}
