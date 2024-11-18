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
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SanPham, SanPhamDtos>();
            CreateMap<SanPhamDtos, SanPham>();
            CreateMap<ChatLieu, ChatLieuDtos>();
            CreateMap<DanhMuc, DanhMucDtos>();
            CreateMap<DiaChi, DiaChiDtos>();
            CreateMap<GiamGium, GiamGiaDtos>();
            CreateMap<GioHangChiTiet, GiohangDtos>()
                    .ForMember(dest => dest.TenSp, src => src.MapFrom(c => c.IdSpctNavigation.IdSpNavigation.TenSp))
                    .ForMember(dest => dest.HinhAnh, src => src.MapFrom(c => c.IdSpctNavigation.IdSpNavigation.ImageDefaul))
                    .ForMember(dest => dest.DonGia, src => src.MapFrom(c => c.IdSpctNavigation.IdSpNavigation.GiaBan))
                     .ForMember(dest => dest.DonGia, src => src.MapFrom(c => c.IdSpctNavigation.IdMsNavigation.TenMauSac))
                       .ForMember(dest => dest.DonGia, src => src.MapFrom(c => c.IdSpctNavigation.IdSizeNavigation.TenSize))
                    .ForMember(dest => dest.TongTien, src => src.MapFrom(c => c.IdSpctNavigation.IdSpNavigation.GiaBan * c.SoLuong));
            CreateMap<AddGioHangDtos, GioHangChiTiet>();
            CreateMap<GioHangUpdate, GioHangChiTiet>();
            
            CreateMap<GiohangDtos, GioHangChiTiet>();
            CreateMap<HoaDon, HoaDonDtos>();
            CreateMap<HoaDonChiTiet, HoaDonChiTietDtos>();
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
