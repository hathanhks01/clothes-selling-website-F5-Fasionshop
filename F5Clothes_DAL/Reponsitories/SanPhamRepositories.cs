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
    public class SanPhamRepositories : ISanPhamRepositories
    {
        private readonly DbduAnTnContext _context;
        public SanPhamRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(SanPham sanPham)
        {
            await _context.SanPhams.AddAsync(sanPham);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idSanPham = await GetById(id);
            _context.SanPhams.Remove(idSanPham);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SanPham>> GetAll()
        {
            return await _context.SanPhams.ToListAsync();
        }

        public async Task<SanPham> GetById(Guid id)
        {
            return await _context.SanPhams.FindAsync(id);
        }

        public async Task Update(SanPham sanPham)
        {
            _context.Entry(sanPham).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
