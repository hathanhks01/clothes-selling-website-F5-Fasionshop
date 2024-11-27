 using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Reponsitories
{
    public class SanPhamRepo : ISanPhamRepo
    {
        private readonly DbduAnTnContext _context;

        public SanPhamRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task<SanPham> AddSanPham(SanPhamDtos sanPhamDto)
        {
            if (sanPhamDto == null)
                throw new ArgumentNullException(nameof(sanPhamDto), "SanPhamDto cannot be null.");

            SanPham sanPham; // Khai báo biến trước khối if-else

            var existingSanPham = await _context.SanPhams
                .Include(sp => sp.SanPhamChiTiets)
                .Include(i => i.Images)
                .FirstOrDefaultAsync(sp => sp.Id == sanPhamDto.Id);

            if (existingSanPham != null)
            {
                // Update sản phẩm
                existingSanPham.MaSp = sanPhamDto.MaSp;
                existingSanPham.TenSp = sanPhamDto.TenSp;
                existingSanPham.GiaBan = sanPhamDto.GiaBan;
                existingSanPham.GiaNhap = sanPhamDto.GiaNhap;
                existingSanPham.DonGiaKhiGiam = sanPhamDto.DonGiaKhiGiam;
                existingSanPham.MoTa = sanPhamDto.MoTa;
                existingSanPham.IdDm = sanPhamDto.IdDm;
                existingSanPham.IdTh = sanPhamDto.IdTh;
                existingSanPham.IdXx = sanPhamDto.IdXx;
                existingSanPham.IdCl = sanPhamDto.IdCl;
                existingSanPham.IdGg = sanPhamDto.IdGg;
                existingSanPham.TheLoai = sanPhamDto.TheLoai;
                existingSanPham.ImageDefaul = sanPhamDto.ImageDefaul;
                existingSanPham.NgayThemGiamGia = sanPhamDto.NgayThemGiamGia;
                existingSanPham.TrangThai = sanPhamDto.TrangThai;

                // Update ChiTietSanPhams
                foreach (var dtoChiTiet in sanPhamDto.ChiTietSanPhams)
                {
                    var chiTiet = existingSanPham.SanPhamChiTiets
                        .FirstOrDefault(ct => ct.IdMs == dtoChiTiet.IdMs && ct.IdSize == dtoChiTiet.IdSize);

                    if (chiTiet != null)
                    {
                        chiTiet.SoLuongTon = dtoChiTiet.SoLuongTon;
                        chiTiet.MoTa = dtoChiTiet.MoTa;
                        chiTiet.TrangThai = dtoChiTiet.TrangThai;
                    }
                    else
                    {
                        existingSanPham.SanPhamChiTiets.Add(new SanPhamChiTiet
                        {
                            IdMs = dtoChiTiet.IdMs,
                            IdSize = dtoChiTiet.IdSize,
                            SoLuongTon = dtoChiTiet.SoLuongTon,
                            MoTa = dtoChiTiet.MoTa,
                            TrangThai = dtoChiTiet.TrangThai
                        });
                    }
                }
                sanPham = existingSanPham;
                await _context.SaveChangesAsync();
            }
            else
            {
                // Thêm mới sản phẩm
                var newSanPhamId = Guid.NewGuid();
                sanPham = new SanPham
                {
                    Id = newSanPhamId,
                    MaSp = sanPhamDto.MaSp,
                    TenSp = sanPhamDto.TenSp,
                    GiaBan = sanPhamDto.GiaBan,
                    GiaNhap = sanPhamDto.GiaNhap,
                    DonGiaKhiGiam = sanPhamDto.DonGiaKhiGiam,
                    MoTa = sanPhamDto.MoTa,
                    IdDm = sanPhamDto.IdDm,
                    IdTh = sanPhamDto.IdTh,
                    IdXx = sanPhamDto.IdXx,
                    IdCl = sanPhamDto.IdCl,
                    IdGg = sanPhamDto.IdGg,
                    TheLoai = sanPhamDto.TheLoai,
                    ImageDefaul = sanPhamDto.ImageDefaul,
                    NgayThem = DateTime.UtcNow,
                    NgayThemGiamGia = sanPhamDto.NgayThemGiamGia,
                    TrangThai = sanPhamDto.TrangThai,
                    SanPhamChiTiets = sanPhamDto.ChiTietSanPhams.Select(dtoChiTiet => new SanPhamChiTiet
                    {
                        IdMs = dtoChiTiet.IdMs,
                        IdSize = dtoChiTiet.IdSize,
                        SoLuongTon = dtoChiTiet.SoLuongTon,
                        MoTa = dtoChiTiet.MoTa,
                        TrangThai = dtoChiTiet.TrangThai
                    }).ToList(),
                    
                };

                await _context.SanPhams.AddAsync(sanPham);
            }

            await _context.SaveChangesAsync();
            return sanPham;
        }


        //public async Task<SanPham> AddSanPham(SanPhamDtos sanPhamDto)
        //{
        //    var sanPham = new SanPham
        //    {
        //        Id = Guid.NewGuid(),
        //        MaSp = sanPhamDto.MaSp,
        //        TenSp = sanPhamDto.TenSp,
        //        GiaBan = sanPhamDto.GiaBan,
        //        GiaNhap = sanPhamDto.GiaNhap,
        //        DonGiaKhiGiam = sanPhamDto.DonGiaKhiGiam,
        //        MoTa = sanPhamDto.MoTa,
        //        IdDm = sanPhamDto.IdDm,
        //        IdTh = sanPhamDto.IdTh,
        //        IdXx = sanPhamDto.IdXx,
        //        IdCl = sanPhamDto.IdCl,
        //        IdGg = sanPhamDto.IdGg,
        //        TheLoai = sanPhamDto.TheLoai,
        //        ImageDefaul = sanPhamDto.ImageDefaul,
        //        NgayThem = DateTime.UtcNow,
        //        NgayThemGiamGia = sanPhamDto.NgayThemGiamGia,
        //        TrangThai = sanPhamDto.TrangThai
        //    };
        //    await _context.SanPhams.AddAsync(sanPham);
        //    _context.SaveChanges();
        //    return sanPham;
        //}

        public async Task DeleteSanPham(Guid id)
        {
            var sanPham = await GetByIdSanPham(id);
            _context.SanPhams.Remove(sanPham);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAllSanPham()
        {
            var result = await _context.SanPhams
                .Include(sp => sp.IdClNavigation) // Bao gồm chất liệu
                .Include(sp => sp.IdThNavigation) // Bao gồm thương hiệu
                .Include(sp => sp.IdDmNavigation) // Bao gồm danh mục
                .Include(sp => sp.SanPhamChiTiets)
                    .ThenInclude(ct => ct.IdMsNavigation) // Bao gồm màu sắc
                .Include(sp => sp.SanPhamChiTiets)
                    .ThenInclude(ct => ct.IdSizeNavigation) // Bao gồm kích thước
                .Select(sp => new
                {
                    sp.Id,
                    sp.TenSp,
                    sp.MaSp,
                    sp.GiaBan,
                    sp.GiaNhap,
                    sp.MoTa,
                    sp.ImageDefaul,
                    sp.NgayThem,
                    sp.TrangThai,
                    DanhMuc = sp.IdDmNavigation == null ? null : new
                    {
                        sp.IdDmNavigation.Id,
                        sp.IdDmNavigation.TenDanhMuc
                    },
                    ThuongHieu = sp.IdThNavigation == null ? null : new
                    {
                        sp.IdThNavigation.Id,
                        sp.IdThNavigation.TenThuongHieu
                    },
                    ChatLieu = sp.IdClNavigation == null ? null : new
                    {
                        sp.IdClNavigation.Id,
                        sp.IdClNavigation.TenChatLieu
                    },
                    SanPhamChiTiets = sp.SanPhamChiTiets.Select(ct => new
                    {
                        ct.Id,
                        ct.SoLuongTon,
                        MauSac = ct.IdMsNavigation == null ? null : new
                        {
                            ct.IdMsNavigation.Id,
                            ct.IdMsNavigation.TenMauSac
                        },
                        KichThuoc = ct.IdSizeNavigation == null ? null : new
                        {
                            ct.IdSizeNavigation.Id,
                            ct.IdSizeNavigation.TenSize
                        }
                    }).ToList()
                })
                .ToListAsync();

            return result;
        }

        public async Task<SanPham> GetByIdSanPham(Guid id)
        {
            return await _context.SanPhams.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SanPham> UpdateSanPham(SanPhamDtos sanPhamDto)
        {
            var existingSanPham = await _context.SanPhams
                .Include(sp => sp.SanPhamChiTiets) // Bao gồm chi tiết sản phẩm
                .Where(sp => sp.Id == sanPhamDto.Id)
                .FirstOrDefaultAsync();

            if (existingSanPham != null)
            {
                existingSanPham.MaSp = sanPhamDto.MaSp;
                existingSanPham.TenSp = sanPhamDto.TenSp;
                existingSanPham.GiaBan = sanPhamDto.GiaBan;
                existingSanPham.GiaNhap = sanPhamDto.GiaNhap;
                existingSanPham.DonGiaKhiGiam = sanPhamDto.DonGiaKhiGiam;
                existingSanPham.MoTa = sanPhamDto.MoTa;
                existingSanPham.IdDm = sanPhamDto.IdDm;
                existingSanPham.IdTh = sanPhamDto.IdTh;
                existingSanPham.IdXx = sanPhamDto.IdXx;
                existingSanPham.IdCl = sanPhamDto.IdCl;
                existingSanPham.IdGg = sanPhamDto.IdGg;
                existingSanPham.TheLoai = sanPhamDto.TheLoai;
                existingSanPham.ImageDefaul = sanPhamDto.ImageDefaul;
                existingSanPham.NgayThem = DateTime.UtcNow; // Cập nhật ngày thêm
                existingSanPham.NgayThemGiamGia = sanPhamDto.NgayThemGiamGia;
                existingSanPham.TrangThai = sanPhamDto.TrangThai;

                // Xử lý ChiTietSanPhams
                var existingChiTietSanPhams = existingSanPham.SanPhamChiTiets.ToList();
                foreach (var chiTiet in existingChiTietSanPhams)
                {
                    var dtoChiTiet = sanPhamDto.ChiTietSanPhams.FirstOrDefault(ct => ct.Id == chiTiet.Id);
                    if (dtoChiTiet != null)
                    {
                        chiTiet.IdMs = dtoChiTiet.IdMs;
                        chiTiet.IdSize = dtoChiTiet.IdSize;
                        chiTiet.SoLuongTon = dtoChiTiet.SoLuongTon;
                        chiTiet.MoTa = dtoChiTiet.MoTa;
                        chiTiet.TrangThai = dtoChiTiet.TrangThai;
                    }
                    else
                    {
                        _context.SanPhamChiTiets.Remove(chiTiet); // Xóa nếu không có trong danh sách DTO
                    }
                }

                foreach (var dtoChiTiet in sanPhamDto.ChiTietSanPhams)
                {
                    if (!existingSanPham.SanPhamChiTiets.Any(ct => ct.Id == dtoChiTiet.Id))
                    {
                        existingSanPham.SanPhamChiTiets.Add(new SanPhamChiTiet
                        {
                            IdMs = dtoChiTiet.IdMs,
                            IdSize = dtoChiTiet.IdSize,
                            SoLuongTon = dtoChiTiet.SoLuongTon,
                            MoTa = dtoChiTiet.MoTa,
                            TrangThai = dtoChiTiet.TrangThai
                        });
                    }
                }

                await _context.SaveChangesAsync();
            }

            return existingSanPham ?? new SanPham();
        }

        public async Task<IEnumerable<object>> GetAllSanPhamsWithDetailsAsync()
        {
            var result = await _context.SanPhams
            .Include(sp => sp.IdClNavigation) // Bao gồm thông tin Chất liệu
            .Include(sp => sp.IdThNavigation) // Bao gồm thông tin Thương hiệu
            .Include(sp => sp.IdDmNavigation) // Bao gồm thông tin Danh mục
            .Include(sp => sp.SanPhamChiTiets)
            .ThenInclude(ct => ct.IdMsNavigation) // Bao gồm thông tin Màu sắc
            .Include(sp => sp.SanPhamChiTiets)
            .ThenInclude(ct => ct.IdSizeNavigation) // Bao gồm thông tin Size
            .Where(sp => sp.TrangThai == 1) // Chỉ lấy các sản phẩm có trạng thái = 1
            .Select(sp => new
            {
                sp.Id,
                sp.TenSp,
                sp.MaSp,
                sp.GiaBan,
                sp.GiaNhap,
                sp.MoTa,
                sp.ImageDefaul,
                sp.NgayThem,
                sp.TrangThai,
                ChatLieu = sp.IdClNavigation == null ? null : new { sp.IdClNavigation.Id, sp.IdClNavigation.TenChatLieu },
                ThuongHieu = sp.IdThNavigation == null ? null : new { sp.IdThNavigation.Id, sp.IdThNavigation.TenThuongHieu },
                DanhMuc = sp.IdDmNavigation == null ? null : new { sp.IdDmNavigation.Id, sp.IdDmNavigation.TenDanhMuc },
                SanPhamChiTiets = sp.SanPhamChiTiets.Select(ct => new
                {
                    ct.Id,
                    ct.SoLuongTon,
                    ct.MoTa,
                    ct.QrCode,
                    ct.TrangThai,
                    MauSac = ct.IdMsNavigation == null ? null : new { ct.IdMsNavigation.Id, ct.IdMsNavigation.TenMauSac },
                    Size = ct.IdSizeNavigation == null ? null : new { ct.IdSizeNavigation.Id, ct.IdSizeNavigation.TenSize }
                }).ToList()
            })
            .ToListAsync();

            return result;
        }


        public async Task<object> GetSanPhamWithDetailsAsync(Guid sanPhamId)
        {
            var result = await _context.SanPhams
                .Where(sp => sp.Id == sanPhamId)
                .Select(sp => new
                {
                    sp.Id,
                    sp.MaSp,
                    sp.TenSp,
                    sp.GiaBan,
                    sp.GiaNhap,
                    sp.NgayThem,
                    sp.NgayThemGiamGia,
                    sp.TheLoai,
                    sp.MoTa,
                    sp.ImageDefaul,
                    sp.TrangThai,
                    SanPhamChiTiets = sp.SanPhamChiTiets
                        .Where(ct => ct.IdSp == sp.Id)
                        .Select(ct => new
                        {
                            ct.Id,
                            MauSac = ct.IdMsNavigation == null ? null : new { ct.IdMsNavigation.Id, ct.IdMsNavigation.TenMauSac },
                            Size = ct.IdSizeNavigation == null ? null : new { ct.IdSizeNavigation.Id, ct.IdSizeNavigation.TenSize },
                            ct.SoLuongTon, // Giả sử có số lượng tồn trong SanPhamChiTiet
                            ct.MoTa,
                        })
                        .ToList(), // Trả về danh sách chi tiết sản phẩm
                    ChatLieu = sp.IdClNavigation == null ? null : new { sp.IdClNavigation.Id, sp.IdClNavigation.TenChatLieu },
                    ThuongHieu = sp.IdThNavigation == null ? null : new { sp.IdThNavigation.Id, sp.IdThNavigation.TenThuongHieu },
                    DanhMuc = sp.IdDmNavigation == null ? null : new { sp.IdDmNavigation.Id, sp.IdDmNavigation.TenDanhMuc },
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
