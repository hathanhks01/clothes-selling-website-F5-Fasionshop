using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class HoaDon
{
    public Guid Id { get; set; }

    public Guid? IdNv { get; set; }

    public Guid? IdKh { get; set; }

    public Guid? IdVouCher { get; set; }

    public string? MaHoaDon { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public DateTime? NgayXacNhan { get; set; }

    public DateTime? NgayChoGiaoHang { get; set; }

    public DateTime? NgayGiaoHang { get; set; }

    public string? DonViGiaoHang { get; set; }

    public string? TenNguoiGiao { get; set; }

    public string? SdtnguoiGiao { get; set; }

    public decimal? TienGiaoHang { get; set; }

    public DateTime? NgayNhanHang { get; set; }

    public string? TenNguoiNhan { get; set; }

    public string? SdtnguoiNhan { get; set; }

    public string? EmailNguoiNhan { get; set; }

    public string? DiaChiNhanHang { get; set; }

    public DateTime? NgayThanhToan { get; set; }

    public DateTime? NgayHuy { get; set; }

    public decimal? GiaTriGiam { get; set; }

    public decimal? TienKhachTra { get; set; }

    public decimal? TienThua { get; set; }

    public decimal? ThanhTien { get; set; }

    public string? GhiChu { get; set; }

    public int? LoaiHoaDon { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<HinhThucThanhToan> HinhThucThanhToans { get; set; } = new List<HinhThucThanhToan>();

    public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();

    public virtual KhachHang? IdKhNavigation { get; set; }

    public virtual NhanVien? IdNvNavigation { get; set; }

    public virtual VouCher? IdVouCherNavigation { get; set; }

    public virtual ICollection<LichSuHoaDon> LichSuHoaDons { get; set; } = new List<LichSuHoaDon>();
}
