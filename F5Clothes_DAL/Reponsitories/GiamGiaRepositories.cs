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
    public class GiamGiaRepositories : IGiamGiaRepositories
    {
        private readonly DbduAnTnContext _context;
        public GiamGiaRepositories(DbduAnTnContext context)
        { 
            _context = context;
        }
        public async Task Create(GiamGia giamGia)
        {
            await _context.GiamGias.AddAsync(giamGia);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idGiamGia = await GetById(id);
            _context.GiamGias.Remove(idGiamGia);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GiamGia>> GetAll()
        {
            return await _context.GiamGias.ToListAsync();
        }

        public async Task<GiamGia> GetById(Guid id)
        {
            return await _context.GiamGias.FindAsync(id);
        }

        public async Task Update(GiamGia giamGia)
        {
            _context.Entry(giamGia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
