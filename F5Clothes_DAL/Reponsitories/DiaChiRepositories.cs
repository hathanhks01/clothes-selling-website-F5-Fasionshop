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
    public class DiaChiRepositories : IDiaChiRepositories
    {
        private readonly DbduAnTnContext _context;
        public DiaChiRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(DiaChi diaChi)
        {
            await _context.DiaChis.AddAsync(diaChi);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idDiaChi = await GetById(id);
            _context.DiaChis.Remove(idDiaChi);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DiaChi>> GetAll()
        {
            return await _context.DiaChis.ToListAsync();
        }

        public async Task<DiaChi> GetById(Guid id)
        {
            return await _context.DiaChis.FindAsync(id);
        }

        public async Task Update(DiaChi diaChi)
        {
            _context.Entry(diaChi).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
