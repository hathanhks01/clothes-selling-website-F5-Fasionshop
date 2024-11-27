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
    public class VoucherRepo: IVoucherRepo
    {
        private readonly DbduAnTnContext _context;
        public VoucherRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddVc(VouCher Vc)
        {
            _context.Add(Vc);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVc(Guid Id)
        {
            var Vc = await GetByVouCher(Id);
            _context.Remove(Vc);
            await _context.SaveChangesAsync();
        }

        public async Task<List<VouCher>> GetAllVouCher()
        {
            return await _context.VouChers.ToListAsync();
        }

        public async Task<VouCher> GetByVouCher(Guid id)
        {
            return await _context.VouChers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<VouCher> GetMaVouCher(string Ma)
        {
            return await _context.VouChers.FirstOrDefaultAsync(x => x.MaVouCher == Ma);
        }
        public async Task UpdateVc(VouCher Vc)
        {
            _context.Entry(Vc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
