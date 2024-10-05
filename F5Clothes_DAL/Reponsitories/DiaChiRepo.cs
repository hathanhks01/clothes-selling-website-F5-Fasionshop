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
    public class DiaChiRepo: IDiaChiRepo
    {
        private readonly DbduAnTnContext _context;
        public DiaChiRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Adddc(DiaChi dc)
        {
            _context.Add(dc);
            await _context.SaveChangesAsync();
        }

        public async Task Deletedc(Guid Id)
        {
            var dc = await GetByDiaChi(Id);
            _context.Remove(dc);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DiaChi>> GetAllDiaChi()
        {
            return await _context.DiaChis.ToListAsync();
        }

        public async Task<DiaChi> GetByDiaChi(Guid id)
        {
            return await _context.DiaChis.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Updatedc(DiaChi dc)
        {
            _context.Entry(dc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
