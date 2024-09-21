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
    public class DanhMucRepositories : IDanhMucRepositories
    {
        private readonly DbduAnTnContext _context;
        public DanhMucRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(DanhMuc danhMuc)
        {
            await _context.DanhMucs.AddAsync(danhMuc);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idDanhMuc = await GetById(id);
            _context.DanhMucs.Remove(idDanhMuc);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DanhMuc>> GetAll()
        {
            return await _context.DanhMucs.ToListAsync();
        }

        public async Task<DanhMuc> GetById(Guid id)
        {
            return await _context.DanhMucs.FindAsync(id);
        }

        public async Task Update(DanhMuc danhMuc)
        {
            _context.Entry(danhMuc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
