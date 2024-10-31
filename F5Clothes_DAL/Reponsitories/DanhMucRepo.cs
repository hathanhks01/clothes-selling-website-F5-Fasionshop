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
    public class DanhMucRepo:IDanhMucRepo
    {
        private readonly DbduAnTnContext _context;
        public DanhMucRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddDm(DanhMuc dm)
        {
            _context.Add(dm);
            await _context.SaveChangesAsync();
        }

        

        public async Task<List<DanhMuc>> GetAllDanhMuc()
        {
            return await _context.DanhMucs.ToListAsync();
        }

        public async Task<DanhMuc> GetByDanhMuc(Guid id)
        {
            // Use FirstOrDefaultAsync to get a single DanhMuc by ID asynchronously
            return await _context.DanhMucs
                                 .Where(x => x.Id == id)
                                 .OrderBy(x => x.TenDanhMuc)
                                 .FirstOrDefaultAsync();
        }


        public async Task UpdateDm(DanhMuc dm)
        {
            _context.Entry(dm).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
