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
    public class VouCherRepositories : IVouCherRepositories
    {
        private readonly DbduAnTnContext _context;
        public VouCherRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(VouCher vouCher)
        {
            await _context.VouChers.AddAsync(vouCher);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idVoucher = await GetById(id);
            _context.VouChers.Remove(idVoucher);
            await _context.SaveChangesAsync();
        }

        public async Task<List<VouCher>> GetAll()
        {
            return await _context.VouChers.ToListAsync();
        }

        public async Task<VouCher> GetById(Guid id)
        {
            return await _context.VouChers.FindAsync(id);
        }

        public async Task Update(VouCher vouCher)
        {
            _context.Entry(vouCher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
