using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using F5Clothes_DAL.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5cvothes_DAL.Reponsitories
{
    public class ChucVuRepo:IChucVuRepo
    {
        private readonly DbduAnTnContext _context;
        public ChucVuRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddCv(ChucVu cv)
        {
            _context.Add(cv);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCv(int Id)
        {
            var cv = await GetByChucVu(Id);
            _context.Remove(cv);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChucVu>> GetAllChucVu()
        {
            return await _context.ChucVus.ToListAsync();
        }

        public async Task<ChucVu> GetByChucVu(int id)
        {
            return await _context.ChucVus.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateCv(ChucVu cv)
        {
            _context.Entry(cv).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
