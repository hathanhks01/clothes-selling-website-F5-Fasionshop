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
            var sanPham = new SanPham
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
                NgayThem = DateTime.UtcNow, // Cập nhật ngày thêm
                NgayThemGiamGia = sanPhamDto.NgayThemGiamGia,
                TrangThai = sanPhamDto.TrangThai
            };
            await _context.SanPhams.AddAsync(sanPham);
            _context.SaveChanges();
            return sanPham;
        }

        public async Task DeleteSanPham(Guid id)
        {
            var sanPham = await GetByIdSanPham(id);
            _context.SanPhams.Remove(sanPham);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SanPham>> GetAllSanPham()
        {
            return await _context.SanPhams.ToListAsync();
        }

        public async Task<SanPham> GetByIdSanPham(Guid id)
        {
            return await _context.SanPhams.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SanPham> UpdateSanPham(SanPhamDtos sanPhamDto)
        {
            var existingSanPham = await _context.SanPhams
                .Where(cl => cl.Id == sanPhamDto.Id)
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

                await _context.SaveChangesAsync();
            }
            return existingSanPham ?? new SanPham();
        }
    }
}
