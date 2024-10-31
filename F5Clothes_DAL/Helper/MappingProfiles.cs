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
            
                CreateMap<SanPham, SanPhamDtos>()
                    .ForMember(dest => dest.TenDanhMuc, opt => opt.MapFrom(src => src.IdDmNavigation != null ? src.IdDmNavigation.TenDanhMuc : null))
                    .ForMember(dest => dest.TenGiamGium, opt => opt.MapFrom(src => src.IdGgNavigation != null ? src.IdGgNavigation.TenGiamGia : null))
                    .ForMember(dest => dest.TenThuongHieu, opt => opt.MapFrom(src => src.IdThNavigation != null ? src.IdThNavigation.TenThuongHieu : null))
                    .ForMember(dest => dest.TenXuatXu, opt => opt.MapFrom(src => src.IdXxNavigation != null ? src.IdXxNavigation.TenXuatXu : null))
                    .ForMember(dest => dest.TenChatLieu, opt => opt.MapFrom(src => src.IdClNavigation != null ? src.IdClNavigation.TenChatLieu : null));


            CreateMap<ChatLieu, ChatLieuDtos>();
            CreateMap<DanhMuc, DanhMucDtos>();
            CreateMap<DiaChi, DiaChiDtos>();
            CreateMap<GiamGium, GiamGiaDtos>();
            CreateMap<GioHang, GiohangDtos>();
            CreateMap<GioHangChiTiet, GioHangChiTietDtos>();
            CreateMap<HoaDon, HoaDonDtos>();
            CreateMap<HoaDonChiTiet, HoaDonChiTietDtos>();
            CreateMap<KhachHang, KhachHangDtos>();
            CreateMap<Image, ImageDtos>();
            CreateMap<LichSuHoaDon, LichSuHoaDonDtos>();
            CreateMap<MauSac, MauSacDtos>();
            CreateMap<NhanVien, NhanVienDtos>();
            CreateMap<RefeshToken, RefeshTokenDtos>();
            CreateMap<SanPhamChiTiet, SanPhamChiTietDtos>()
             .ForMember(dest => dest.TenSp, opt => opt.MapFrom(src => src.IdSpNavigation.TenSp))
             .ReverseMap();

            CreateMap<Size, SizeDtos>();
            CreateMap<XuatXu, XuatXuDtos>();
            CreateMap<ThuongHieu, ThuongHieuDtos>();
            CreateMap<VouCher, VouCherDtos>();
            CreateMap<ChucVu, ChuVuDtos>();
            CreateMap<HinhThucThanhToan, HinhThucThanhToanDtos>();
        }
    }
}
