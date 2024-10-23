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
    public class MauSacRepositories : IMauSacRepositories
    {
        private readonly DbduAnTnContext _context;
        public MauSacRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(MauSac mauSac)
        {
            await _context.MauSacs.AddAsync(mauSac);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idMauSac = await GetById(id);
            _context.MauSacs.Remove(idMauSac);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MauSac>> GetAll()
        {
            return await _context.MauSacs.ToListAsync();
        }

        public async Task<MauSac> GetById(Guid id)
        {
            return await _context.MauSacs.FindAsync(id);
        }

        public async Task Update(MauSac mauSac)
        {
            _context.Entry(mauSac).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
