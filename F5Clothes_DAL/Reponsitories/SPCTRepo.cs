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
    public class SPCTRepo : ISPCTRepo
    {
        private readonly DbduAnTnContext _context;
        public SPCTRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task<SanPhamChiTiet> AddSanPhamChiTiet(SanPhamChiTietDtos sanPhamChiTietDto)
        {
            var sanPhamChiTiet = new SanPhamChiTiet
            {
                Id = Guid.NewGuid(),
                SoLuongTon = sanPhamChiTietDto.SoLuongTon,
                MoTa = sanPhamChiTietDto.MoTa,
                TrangThai = sanPhamChiTietDto.TrangThai,
                IdSp = sanPhamChiTietDto.IdSp,
                IdMs = sanPhamChiTietDto.IdMs,
                IdSize = sanPhamChiTietDto.IdSize,
                NgayTao = DateTime.UtcNow, // Cập nhật ngày thêm
                QrCode = sanPhamChiTietDto.QrCode,
            };
            await _context.SanPhamChiTiets.AddAsync(sanPhamChiTiet);
            _context.SaveChanges();
            return sanPhamChiTiet;
        }

        public async Task DeleteSanPhamChiTiet(Guid id)
        {
            var sanPhamChiTietDto = await GetByIdSanPhamChiTiet(id);
            _context.SanPhamChiTiets.Remove(sanPhamChiTietDto);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SanPhamChiTiet>> GetAllSanPhamChiTiet()
        {
            return await _context.SanPhamChiTiets.ToListAsync();
        }

        public async Task<SanPhamChiTiet> GetByIdSanPhamChiTiet(Guid id)
        {
            return await _context.SanPhamChiTiets.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SanPhamChiTiet> UpdateSanPhamChiTiet(SanPhamChiTietDtos sanPhamChiTietDto)
        {            
            var existingSPCT = await _context.SanPhamChiTiets
                .Where(cl => cl.Id == sanPhamChiTietDto.Id)
                .FirstOrDefaultAsync();
            if (existingSPCT != null)
            {
                existingSPCT.SoLuongTon = sanPhamChiTietDto.SoLuongTon;
                existingSPCT.MoTa = sanPhamChiTietDto.MoTa;
                existingSPCT.TrangThai = sanPhamChiTietDto.TrangThai;
                existingSPCT.IdSp = sanPhamChiTietDto.IdSp;
                existingSPCT.IdMs = sanPhamChiTietDto.IdMs;
                existingSPCT.IdSize = sanPhamChiTietDto.IdSize;
                existingSPCT.NgayTao = DateTime.UtcNow; // Cập nhật ngày thêm
                existingSPCT.QrCode = sanPhamChiTietDto.QrCode;

                await _context.SaveChangesAsync();
            }
            return existingSPCT ?? new SanPhamChiTiet();
        }
    }
}
