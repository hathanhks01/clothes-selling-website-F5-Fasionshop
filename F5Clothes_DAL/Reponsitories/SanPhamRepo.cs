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

            var existingSanPham = await _context.SanPhams
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
            }
            else
            {
                // Thêm mới sản phẩm
                existingSanPham = new SanPham
                {
                    Id = Guid.NewGuid(),
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
                    TrangThai = sanPhamDto.TrangThai
                };

                await _context.SanPhams.AddAsync(existingSanPham);
            }

            await _context.SaveChangesAsync();
            return existingSanPham;
        }



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
                        Size = ct.IdSizeNavigation == null ? null : new
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
            if (sanPhamDto == null) throw new ArgumentNullException(nameof(sanPhamDto));

            var existingSanPham = await _context.SanPhams
                .FirstOrDefaultAsync(sp => sp.Id == sanPhamDto.Id);

            if (existingSanPham == null) throw new InvalidOperationException("Sản phẩm không tìm thấy.");

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

            await _context.SaveChangesAsync();
            return existingSanPham;
        }

        public async Task<IEnumerable<object>> GetAllSanPhamsWithDetailsAsync()
        {
            var result = await _context.SanPhams
            .Include(sp => sp.IdClNavigation)
            .Include(sp => sp.IdThNavigation)
            .Include(sp => sp.IdDmNavigation)
            .Include(sp => sp.SanPhamChiTiets)
            .ThenInclude(ct => ct.IdMsNavigation)
            .Include(sp => sp.SanPhamChiTiets)
            .ThenInclude(ct => ct.IdSizeNavigation)
            .Where(sp => sp.TrangThai == 1)
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
                    sp.MoTa,
                    sp.ImageDefaul,
                    XuatXu = sp.IdXxNavigation == null ? null : new { sp.IdXxNavigation.Id, sp.IdXxNavigation.TenXuatXu },
                    DanhMuc = sp.IdDmNavigation == null ? null : new { sp.IdDmNavigation.Id, sp.IdDmNavigation.TenDanhMuc },
                    ThuongHieu = sp.IdThNavigation == null ? null : new { sp.IdThNavigation.Id, sp.IdThNavigation.TenThuongHieu },
                    ChatLieu = sp.IdClNavigation == null ? null : new { sp.IdClNavigation.Id, sp.IdClNavigation.TenChatLieu },
                    MauSac = sp.SanPhamChiTiets
                        .Where(ct => ct.IdSp == sp.Id)
                        .Select(ct => new
                        {
                            SanPhamChiTietId = ct.Id, // ID của SanPhamChiTiet
                            MauSacId = ct.IdMsNavigation.Id,
                            MauSacTen = ct.IdMsNavigation.TenMauSac
                        })
                        .Where(ct => ct.MauSacId != null)
                        .Distinct()
                        .ToList(), // Trả về danh sách màu sắc và ID của SanPhamChiTiet
                    Size = sp.SanPhamChiTiets
                        .Where(ct => ct.IdSp == sp.Id)
                        .Select(ct => new
                        {
                            SanPhamChiTietId = ct.Id, // ID của SanPhamChiTiet
                            SizeId = ct.IdSizeNavigation.Id,
                            SizeTen = ct.IdSizeNavigation.TenSize,
                            SoLuongTon = ct.SoLuongTon

                        })
                        .Where(ct => ct.SizeId != null)
                        .Distinct()
                        .ToList(),
                    Images = sp.Images
                        .Where(image => image.IdSp == sp.Id)
                        .Select(image => new { image.Id, image.TenImage })
                        .Distinct()
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return result;
        }
        public async Task<SanPhamChiTiet> AddOrUpdateSanPhamChiTiet(SanPhamChiTietDtos chiTietDtos)
        {
            if (chiTietDtos == null)
                throw new ArgumentNullException(nameof(chiTietDtos), "SanPhamDto cannot be null.");

            var existingSanPham = chiTietDtos.Id != Guid.Empty
            ? await _context.SanPhamChiTiets.FirstOrDefaultAsync(sp => sp.Id == chiTietDtos.Id): null;

            if (existingSanPham != null)
            {
                existingSanPham.IdMs = chiTietDtos.IdMs;
                existingSanPham.IdSize = chiTietDtos.IdSize;
                existingSanPham.SoLuongTon = chiTietDtos.SoLuongTon;
            }
            else
            {
                existingSanPham = new SanPhamChiTiet
                {
                    Id = Guid.NewGuid(),
                    IdSp = chiTietDtos.IdSp,
                    IdMs = chiTietDtos.IdMs,
                    IdSize = chiTietDtos.IdSize,
                    SoLuongTon = chiTietDtos.SoLuongTon,
                    TrangThai = chiTietDtos.TrangThai,
                    MoTa = chiTietDtos.MoTa,
                    NgayTao = chiTietDtos.NgayTao,
                    QrCode = chiTietDtos.QrCode
                };

                await _context.SanPhamChiTiets.AddAsync(existingSanPham);
            }

            await _context.SaveChangesAsync();
            return existingSanPham;
        }
        public async Task UpdateSanPhamChiTiet(Guid sanPhamId, IEnumerable<SanPhamChiTietDtos> chiTietDtos)
        {
            var existingSanPham = await _context.SanPhams
                .Include(sp => sp.SanPhamChiTiets)
                .FirstOrDefaultAsync(sp => sp.Id == sanPhamId);

            if (existingSanPham == null) throw new InvalidOperationException("Sản phẩm không tìm thấy.");

            var existingChiTietSanPhams = existingSanPham.SanPhamChiTiets.ToList();

            foreach (var chiTiet in existingChiTietSanPhams)
            {
                var dtoChiTiet = chiTietDtos.FirstOrDefault(ct => ct.Id == chiTiet.Id);
                if (dtoChiTiet != null)
                {
                    chiTiet.IdMs = dtoChiTiet.IdMs;
                    chiTiet.IdSize = dtoChiTiet.IdSize;
                    chiTiet.SoLuongTon = dtoChiTiet.SoLuongTon;
                    chiTiet.TrangThai = dtoChiTiet.TrangThai;
                    chiTiet.MoTa = dtoChiTiet.MoTa;
                    chiTiet.NgayTao = dtoChiTiet.NgayTao;
                    chiTiet.QrCode = dtoChiTiet.QrCode;
                }
                else
                {
                    _context.SanPhamChiTiets.Remove(chiTiet);
                }
            }

            // Thêm chi tiết mới
            foreach (var dtoChiTiet in chiTietDtos)
            {
                if (!existingSanPham.SanPhamChiTiets.Any(ct => ct.Id == dtoChiTiet.Id))
                {
                    existingSanPham.SanPhamChiTiets.Add(new SanPhamChiTiet
                    {
                        Id = dtoChiTiet.Id != Guid.Empty ? dtoChiTiet.Id : Guid.NewGuid(),
                        IdMs = dtoChiTiet.IdMs,
                        IdSize = dtoChiTiet.IdSize,
                        SoLuongTon = dtoChiTiet.SoLuongTon,
                        MoTa = dtoChiTiet.MoTa,
                        QrCode = dtoChiTiet.QrCode,
                        TrangThai = dtoChiTiet.TrangThai,
                        NgayTao = dtoChiTiet.NgayTao ?? DateTime.UtcNow, 
                    });
                }
            }

            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<SanPhamChiTiet>> GetSanPhamChiTietBySanPhamId(Guid sanPhamId)
        {
            var chiTietSanPhams = await _context.SanPhamChiTiets
                .Where(sct => sct.IdSp == sanPhamId)
                .Select(sct => new SanPhamChiTiet
                {
                    Id = sct.Id,
                    IdSp = sct.IdSp,
                    IdMs = sct.IdMs,
                    IdSize = sct.IdSize,
                    SoLuongTon = sct.SoLuongTon,
                    MoTa = sct.MoTa,
                    TrangThai = sct.TrangThai
                })
                .ToListAsync();

            return chiTietSanPhams;
        }

        public async Task<SanPhamChiTiet> GetByIdSanPhamChiTiet(Guid id)
        {
            return await _context.SanPhamChiTiets.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<object>> GetAllImageBySanPham()
        {
            var result = await _context.SanPhams
                .Include(sp => sp.Images) // Bao gồm thương hiệu
                .Select(sp => new
                {
                    sp.Id,
                    sp.TenSp,
                    sp.MaSp,
                    sp.ImageDefaul,
                    sp.TrangThai,
                    Images = sp.Images
                        .Where(image => image.IdSp == sp.Id)
                        .Select(image => new { image.Id, image.TenImage })
                        .Distinct()
                   .ToList()
                })
                .ToListAsync();

            return result;
        }

        public async Task<Image> AddOrUpdateHinhAnhChiTiet(ImageDtos chiTietDtos)
        {
            if (chiTietDtos == null)
                throw new ArgumentNullException(nameof(chiTietDtos), "Image details cannot be null.");
            var existingImage = chiTietDtos.Id != Guid.Empty
                ? await _context.Images.FirstOrDefaultAsync(img => img.Id == chiTietDtos.Id)
                : null;

            if (existingImage != null)
            {
                existingImage.TenImage = chiTietDtos.TenImage;
                existingImage.MoTa = chiTietDtos.MoTa;
                existingImage.TrangThai = chiTietDtos.TrangThai;
            }
            else
            {
                existingImage = new Image
                {
                    Id = chiTietDtos.Id == Guid.Empty ? Guid.NewGuid() : chiTietDtos.Id,
                    IdSp = chiTietDtos.IdSp,
                    TenImage = chiTietDtos.TenImage,
                    MoTa = chiTietDtos.MoTa,
                    TrangThai = chiTietDtos.TrangThai
                };

                await _context.Images.AddAsync(existingImage);
            }
            await _context.SaveChangesAsync();
            return existingImage;
        }

    }
}
