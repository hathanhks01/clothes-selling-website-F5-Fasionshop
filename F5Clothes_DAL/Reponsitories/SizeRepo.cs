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
    public class SizeRepo : ISizeRepo
    {
        private readonly DbduAnTnContext _context;
        public SizeRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task<Size> AddSize(SizeDtos sizeDto)
        {
            var size = new Size
            {
                Id = Guid.NewGuid(),
                TenSize = sizeDto.TenSize,
                MoTa = sizeDto.MoTa,
                TrangThai = sizeDto.TrangThai
            };
            await _context.Sizes.AddAsync(size);
            _context.SaveChanges();
            return size;
        }

        public async Task DeleteSize(Guid id)
        {
            var size = await GetByIdSize(id);
            _context.Sizes.Remove(size);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Size>> GetAllSize()
        {
            return await _context.Sizes.ToListAsync();
        }

        public async Task<Size> GetByIdSize(Guid id)
        {
            return await _context.Sizes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Size> UpdateSize(SizeDtos sizeDto)
        {
            var existingSize = await _context.Sizes
                .Where(cl => cl.Id == sizeDto.Id)
                .FirstOrDefaultAsync();
            if (existingSize != null)
            {
                existingSize.TenSize = sizeDto.TenSize;
                existingSize.MoTa = sizeDto.MoTa;
                existingSize.TrangThai = sizeDto.TrangThai;

                await _context.SaveChangesAsync();
            }
            return existingSize ?? new Size();
        }
    }
}
