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
    public class XuatXuRepositories : IXuatXuRepositories
    {
        private readonly DbduAnTnContext _context;
        public XuatXuRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(XuatXu xuatXu)
        { 
            await _context.XuatXus.AddAsync(xuatXu);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idXuatXu = await GetById(id);
            _context.XuatXus.Remove(idXuatXu);
            await _context.SaveChangesAsync();
        }

        public async Task<List<XuatXu>> GetAll()
        {
            return await _context.XuatXus.ToListAsync();
        }

        public async Task<XuatXu> GetById(Guid id)
        {
            return await _context.XuatXus.FindAsync();
        }

        public async Task Update(XuatXu xuatXu)
        {
            _context.Entry(xuatXu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
