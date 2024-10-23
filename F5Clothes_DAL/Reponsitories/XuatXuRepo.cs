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
    public class XuatXuRepo: IXuatXuRepo
    {
        private readonly DbduAnTnContext _context;
        public XuatXuRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddXx(XuatXu xx)
        {
            _context.Add(xx);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteXx(Guid Id)
        {
            var xx = await GetByXuatXu(Id);
            _context.Remove(xx);
            await _context.SaveChangesAsync();
        }

        public async Task<List<XuatXu>> GetAllXuatXu()
        {
            return await _context.XuatXus.ToListAsync();
        }

        public async Task<XuatXu> GetByXuatXu(Guid id)
        {
            return await _context.XuatXus.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateXx(XuatXu xx)
        {
            _context.Entry(xx).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
