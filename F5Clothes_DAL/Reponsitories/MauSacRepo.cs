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
    public class MauSacRepo : IMauSacRepo
    {
        private readonly DbduAnTnContext _context;
        public MauSacRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task<MauSac> AddMauSac(MauSacDtos mauSacDto)
        {
            var mauSac = new MauSac
            {
                Id = Guid.NewGuid(),
                TenMauSac = mauSacDto.TenMauSac,
                MoTa = mauSacDto.MoTa,
                TrangThai = mauSacDto.TrangThai
            };
            await _context.MauSacs.AddAsync(mauSac);
            _context.SaveChanges();
            return mauSac;
        }

        public async Task DeleteMauSac(Guid id)
        {
            var mauSac = await GetByIdMauSac(id);
            _context.MauSacs.Remove(mauSac);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MauSac>> GetAllMauSac()
        {
            return await _context.MauSacs.ToListAsync();
        }

        public async Task<MauSac> GetByIdMauSac(Guid id)
        {
            return await _context.MauSacs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MauSac> UpdateMauSac(MauSacDtos mauSacDto)
        {
            var existingMauSac = await _context.MauSacs
                .Where(cl => cl.Id == mauSacDto.Id)
                .FirstOrDefaultAsync();
            if (existingMauSac != null)
            {
                existingMauSac.TenMauSac = mauSacDto.TenMauSac;
                existingMauSac.MoTa = mauSacDto.MoTa;
                existingMauSac.TrangThai = mauSacDto.TrangThai;

                await _context.SaveChangesAsync();
            }
            return existingMauSac ?? new MauSac();
        }
    }
}
