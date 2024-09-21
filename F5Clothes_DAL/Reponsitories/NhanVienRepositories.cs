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
    public class NhanVienRepositories : INhanVienRepositories
    {
        private readonly DbduAnTnContext _context;
        public NhanVienRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task<List<NhanVien>> GetAll()
        {
            return await _context.NhanViens.ToListAsync();
        }

        public async Task<NhanVien> GetById(Guid id)
        {
            return await _context.NhanViens.FindAsync(id);
        }

        public async Task Create(NhanVien nhanVien)
        {
            await _context.NhanViens.AddAsync(nhanVien);
            await _context.SaveChangesAsync();
        }

        public async Task Update(NhanVien nhanVien)
        {
            _context.Entry(nhanVien).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idNhanVien = await GetById(id);
            _context.NhanViens.Remove(idNhanVien);
            await _context.SaveChangesAsync();
        }
       
    }
}
