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
    public class KhachHangRepositories : IKhachHangRepositories
    {
        private readonly DbduAnTnContext _context;
        public KhachHangRepositories(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task Create(KhachHang khachHang)
        {
            await _context.KhachHangs.AddAsync(khachHang);
            await _context.SaveChangesAsync();
        }

        public  async Task Delete(Guid id)
        {
            var idKhachHang = await GetById(id);
            _context.KhachHangs.Remove(idKhachHang);
            await _context.SaveChangesAsync();
        }

        public async Task<List<KhachHang>> GetAll()
        {
            return await _context.KhachHangs.ToListAsync();
        }

        public async Task<KhachHang> GetById(Guid id)
        {
            return await _context.KhachHangs.FindAsync(id);
        }

        public async Task Update(KhachHang khachHang)
        {
            _context.Entry(khachHang).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
