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
    public class GiamGiaRepo: IGiamGiaRepo
    {
        private readonly DbduAnTnContext _context;
        public GiamGiaRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddGg(GiamGium Gg)
        {
            _context.Add(Gg);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGg(Guid Id)
        {
            var Gg = await GetByGg(Id);
            _context.Remove(Gg);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GiamGium>> GetAllGg()
        {
            return await _context.GiamGia.ToListAsync();
        }

        public async Task<GiamGium> GetByGg(Guid id)
        {
            return await _context.GiamGia.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateGg(GiamGium Gg)
        {
            _context.Entry(Gg).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
