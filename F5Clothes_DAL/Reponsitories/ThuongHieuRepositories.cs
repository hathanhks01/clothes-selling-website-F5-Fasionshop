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
    public class ThuongHieuRepositories : IThuongHieuRepositories
    {
        private readonly DbduAnTnContext _context;
        public ThuongHieuRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(ThuongHieu thuongHieu)
        {
            await _context.ThuongHieus.AddAsync(thuongHieu);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idThuongHieu = await GetById(id);
            _context.ThuongHieus.Remove(idThuongHieu);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ThuongHieu>> GetAll()
        {
            return await _context.ThuongHieus.ToListAsync();
        }

        public async Task<ThuongHieu> GetById(Guid id)
        {
            return await _context.ThuongHieus.FindAsync(id);
        }

        public async Task Update(ThuongHieu thuongHieu)
        {
            _context.Entry(thuongHieu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
