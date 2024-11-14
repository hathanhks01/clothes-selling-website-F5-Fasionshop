using AutoMapper;

using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<SanPham, SanPhamDtos>();
            CreateMap<SanPhamDtos, SanPham>();
            CreateMap<ChatLieu, ChatLieuDtos>();
            CreateMap<DanhMuc, DanhMucDtos>();
            CreateMap<DiaChi, DiaChiDtos>();
            CreateMap<GiamGium, GiamGiaDtos>();
            CreateMap<GioHang, GiohangDtos>();
            CreateMap<GioHangChiTiet, GioHangChiTietDtos>();
            CreateMap<HoaDon, HoaDonDtos>();
            CreateMap<HoaDonChiTiet,HoaDonChiTietDtos>();
            CreateMap<KhachHang, KhachHangDtos>();
            CreateMap<KhachHangDtos, KhachHang>();
            CreateMap<Image, ImageDtos>();
            CreateMap<LichSuHoaDon, LichSuHoaDonDtos>();
            CreateMap<MauSac, MauSacDtos>();
            CreateMap<NhanVien, NhanVienDtos>();
            CreateMap<RefeshToken, RefeshTokenDtos>();
            CreateMap<SanPhamChiTiet, SanPhamChiTietDtos>();
            CreateMap<Size, SizeDtos>();
            CreateMap<XuatXu, XuatXuDtos>();
            CreateMap<ThuongHieu, ThuongHieuDtos>();
            CreateMap<VouCher, VouCherDtos>();
            CreateMap<ChucVu, ChuVuDtos>();
            CreateMap<HinhThucThanhToan, HinhThucThanhToanDtos>();
        }
    }
}
