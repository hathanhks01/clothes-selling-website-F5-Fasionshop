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
            // Map from GioHangChiTiet to GiohangDtos
            CreateMap<GioHangChiTiet, GiohangDtos>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TenSp, opt => opt.MapFrom(src => src.IdSpctNavigation.IdSpNavigation.TenSp))  // Mapping for product name
                .ForMember(dest => dest.HinhAnh, opt => opt.MapFrom(src => src.IdSpctNavigation.IdSpNavigation.ImageDefaul))  // Mapping for image
                .ForMember(dest => dest.DonGia, opt => opt.MapFrom(src => src.IdSpctNavigation.IdSpNavigation.GiaBan))  // Mapping for price
                .ForMember(dest => dest.TenMauSac, opt => opt.MapFrom(src => src.IdSpctNavigation.IdMsNavigation.TenMauSac))  // Mapping for color
                .ForMember(dest => dest.TenSize, opt => opt.MapFrom(src => src.IdSpctNavigation.IdSizeNavigation.TenSize))  // Mapping for size
                .ForMember(dest => dest.TongTien, opt => opt.MapFrom(src => src.IdSpctNavigation.IdSpNavigation.GiaBan * src.SoLuong));  // Mapping total price

            // Map from AddGioHangDtos to GioHangChiTiet (likely for creating cart items)
            CreateMap<AddGioHangDtos, GioHangChiTiet>();

            // Map from GioHangUpdate to GioHangChiTiet (for updating cart items)
            
            CreateMap<GiohangDtos, GioHangChiTiet>()
                       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IdGh, opt => opt.MapFrom(src => src.IdGh))
            .ForMember(dest => dest.IdSpct, opt => opt.MapFrom(src => src.IdSpct))
            .ForMember(dest => dest.DonGia, opt => opt.MapFrom(src => src.DonGia))
            .ForMember(dest => dest.DonGiaKhiGiam, opt => opt.MapFrom(src => src.DonGiaKhiGiam))
            .ForMember(dest => dest.SoLuong, opt => opt.MapFrom(src => src.SoLuong))
            
            .ForMember(dest => dest.GhiChu, opt => opt.Ignore()) // Cũng có thể bỏ qua nếu không cần
            .ForMember(dest => dest.TrangThai, opt => opt.Ignore()) // Cũng có thể bỏ qua nếu không cần
            .ForMember(dest => dest.NgayTao, opt => opt.Ignore()) // Cũng có thể bỏ qua nếu không cần
            .ForMember(dest => dest.NgayCapNhat, opt => opt.Ignore()) // Bỏ qua nếu không có trong DTO
            .ForMember(dest => dest.IdGhNavigation, opt => opt.Ignore()) // Không cần ánh xạ liên kết
            .ForMember(dest => dest.IdSpctNavigation, opt => opt.Ignore());

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
