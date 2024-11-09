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
    public class XuatXuRepo : IXuatXuRepo
    {
        private readonly DbduAnTnContext _context;
        public XuatXuRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task<XuatXu> AddXuatXu(XuatXuDtos xuatXuDto)
        {
            var xuatXu = new XuatXu
            {
                Id = Guid.NewGuid(),
                TenXuatXu = xuatXuDto.TenXuatXu,
                MoTa = xuatXuDto.MoTa,
                TrangThai = xuatXuDto.TrangThai
            };
            await _context.XuatXus.AddAsync(xuatXu);
            _context.SaveChanges();
            return xuatXu;
        }

        public async Task DeleteXuatXu(Guid id)
        {
            var xuatXu = await GetByIdXuatXu(id);
            _context.XuatXus.Remove(xuatXu);
            await _context.SaveChangesAsync();
        }

        public async Task<List<XuatXu>> GetAllXuatXu()
        {
            return await _context.XuatXus.ToListAsync();
        }

        public async Task<XuatXu> GetByIdXuatXu(Guid id)
        {
            return await _context.XuatXus.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<XuatXu> UpdateXuatXu(XuatXuDtos xuatXuDto)
        {
            var existiingXuatXu = await _context.XuatXus
                .Where(xuatXu => xuatXu.Id == xuatXuDto.Id)
                .FirstOrDefaultAsync();
            if (existiingXuatXu != null)
            {
                existiingXuatXu.TenXuatXu = xuatXuDto.TenXuatXu;
                existiingXuatXu.MoTa = xuatXuDto.MoTa;
                existiingXuatXu.TrangThai = xuatXuDto.TrangThai;

                await _context.SaveChangesAsync();
            }
            return existiingXuatXu ?? new XuatXu();
        }
    }
}
