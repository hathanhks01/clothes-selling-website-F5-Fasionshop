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
    public class ThuongHieuRepo: IThuongHieuRepo
    {
        private readonly DbduAnTnContext _context;
        public ThuongHieuRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddTh(ThuongHieu th)
        {
            _context.Add(th);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTh(Guid Id)
        {
            var th = await GetByThuongHieu(Id);
            _context.Remove(th);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ThuongHieu>> GetAllThuongHieu()
        {
            return await _context.ThuongHieus.ToListAsync();
        }

        public async Task<ThuongHieu> GetByThuongHieu(Guid id)
        {
            return await _context.ThuongHieus.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateTh(ThuongHieu th)
        {
            _context.Entry(th).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
