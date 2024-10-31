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
    public class ThuongHieuRepo : IThuongHieuRepo
    {
        private readonly DbduAnTnContext _context;
        public ThuongHieuRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task<ThuongHieu> AddThuongHieu(ThuongHieuDtos thuongHieuDto)
        {
            var thuongHieu = new ThuongHieu
            {
                Id = Guid.NewGuid(),
                TenThuongHieu = thuongHieuDto.TenThuongHieu,
                MoTa = thuongHieuDto.MoTa,
                TrangThai = thuongHieuDto.TrangThai
            };
            await _context.ThuongHieus.AddAsync(thuongHieu);
            _context.SaveChanges();
            return thuongHieu;
        }

        public async Task DeleteThuongHieu(Guid id)
        {
            var thuongHieu = await GetByIdThuongHieu(id);
            _context.ThuongHieus.Remove(thuongHieu);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ThuongHieu>> GetAllThuongHieu()
        {
            return await _context.ThuongHieus.ToListAsync();
        }

        public async Task<ThuongHieu> GetByIdThuongHieu(Guid id)
        {
            return await _context.ThuongHieus.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ThuongHieu> UpdateThuongHieu(ThuongHieuDtos thuongHieuDto)
        {
            var existingThuongHieu = await _context.ThuongHieus
                .Where(thuongHieu => thuongHieu.Id == thuongHieuDto.Id)
                .FirstOrDefaultAsync();
            if (existingThuongHieu != null)
            {
                existingThuongHieu.TenThuongHieu = thuongHieuDto.TenThuongHieu;
                existingThuongHieu.MoTa = thuongHieuDto.MoTa;
                existingThuongHieu.TrangThai = thuongHieuDto.TrangThai;

                await _context.SaveChangesAsync();
            }
            return existingThuongHieu ?? new ThuongHieu();
        }
    }
}
