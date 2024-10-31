using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Reponsitories
{
    public class GiamGiaRepo : IGiamGiaRepo
    {
        private readonly DbduAnTnContext _context;
        public GiamGiaRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task<GiamGium> AddGiamGia(GiamGiaDtos giamGiaDto)
        {
            var giamGia = new GiamGium
            {
                Id = Guid.NewGuid(),
                MaGiamGia = giamGiaDto.MaGiamGia,
                TenGiamGia = giamGiaDto.TenGiamGia,
                NgayTao = DateTime.UtcNow,
                NgayCapNhat = giamGiaDto.NgayCapNhat,
                NgayBatDau = giamGiaDto.NgayBatDau,
                NgayKetThuc = giamGiaDto.NgayKetThuc,
                GiaTriGiam = giamGiaDto.GiaTriGiam,
                HinhThucGiam = giamGiaDto.HinhThucGiam,
                GhiChu = giamGiaDto.GhiChu,
                TrangThai = giamGiaDto.TrangThai
            };
            await _context.GiamGia.AddAsync(giamGia);
            _context.SaveChanges();
            return giamGia;
        }

        public async Task DeleteGiamGia(Guid id)
        {
            var giamGia = await GetByIdGiamGia(id);
            _context.GiamGia.Remove(giamGia);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GiamGium>> GetAllGiamGia()
        {
            return await _context.GiamGia.ToListAsync();
        }

        public async Task<GiamGium> GetByIdGiamGia(Guid id)
        {
            return await _context.GiamGia.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GiamGium> UpdateGiamGia(GiamGiaDtos giamGiaDto)
        {
            var existingGiamGia = await _context.GiamGia
               .Where(gg => gg.Id == giamGiaDto.Id)
               .FirstOrDefaultAsync();
            if (existingGiamGia != null)
            {
                existingGiamGia.MaGiamGia = giamGiaDto.MaGiamGia;
                existingGiamGia.TenGiamGia = giamGiaDto.TenGiamGia;
                existingGiamGia.NgayTao = DateTime.UtcNow;
                existingGiamGia.NgayCapNhat = giamGiaDto.NgayCapNhat;
                existingGiamGia.NgayBatDau = giamGiaDto.NgayBatDau;
                existingGiamGia.NgayKetThuc = giamGiaDto.NgayKetThuc;
                existingGiamGia.GiaTriGiam = giamGiaDto.GiaTriGiam;
                existingGiamGia.HinhThucGiam = giamGiaDto.HinhThucGiam;
                existingGiamGia.GhiChu = giamGiaDto.GhiChu;
                existingGiamGia.TrangThai = giamGiaDto.TrangThai;
            }
                await _context.SaveChangesAsync();         
            return existingGiamGia ?? new GiamGium();
        }
    }
}
