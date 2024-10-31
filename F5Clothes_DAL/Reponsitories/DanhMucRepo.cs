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
    public class DanhMucRepo : IDanhMucRepo
    {
        private readonly DbduAnTnContext _context;
        public DanhMucRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task<DanhMuc> AddDanhMuc(DanhMucDtos danhMucDto)
        {
            var danhMuc = new DanhMuc
            {
                Id = Guid.NewGuid(),
                TenDanhMuc = danhMucDto.TenDanhMuc,
                MoTa = danhMucDto.MoTa,
                TrangThai = danhMucDto.TrangThai
            };
            await _context.DanhMucs.AddAsync(danhMuc);
            _context.SaveChanges();
            return danhMuc;
        }

        public async Task DeleteDanhMuc(Guid id)
        {
            var danhMuc = await GetByIdDanhMuc(id);
            _context.DanhMucs.Remove(danhMuc);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DanhMuc>> GetAllDanhMuc()
        {
            return await _context.DanhMucs.ToListAsync();
        }

        public async Task<DanhMuc> GetByIdDanhMuc(Guid id)
        {
            return await _context.DanhMucs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DanhMuc> UpdateDanhMuc(DanhMucDtos danhMucDto)
        {
            var existingDanhMuc = await _context.DanhMucs
                .Where(danhMuc => danhMuc.Id == danhMucDto.Id)
                .FirstOrDefaultAsync();
            if (existingDanhMuc != null)
            {
                existingDanhMuc.TenDanhMuc = danhMucDto.TenDanhMuc;
                existingDanhMuc.MoTa = danhMucDto.MoTa;
                existingDanhMuc.TrangThai = danhMucDto.TrangThai;

                await _context.SaveChangesAsync();
            }
            return existingDanhMuc ?? new DanhMuc();
        }
    }
}
