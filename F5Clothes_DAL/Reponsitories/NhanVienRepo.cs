using Azure.Core;
using F5Clothes_DAL.DTOs;
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
    public class NhanVienRepo : INhanVienRepo
    {
        private readonly DbduAnTnContext _context;
        public NhanVienRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        
        public async Task<List<NhanVien>> GetAllNhanVien()
        {
            return await _context.NhanViens.ToListAsync();
        }

        public async Task DeleteNhanVien(string Id)
        {
            var sp = await GetByNhanVien(Id);
			if(sp != null)
			{
				_context.NhanViens.Remove(sp);
				await _context.SaveChangesAsync();
			}
        }

        public async Task<NhanVien> GetByNhanVien(string Id)
        {
            return await _context.NhanViens.FirstOrDefaultAsync(x => x.MaNv == Id);
        }

		public async Task AddNhanVien(NhanVien nv)
		{
            await _context.NhanViens.AddAsync(nv);
            await _context.SaveChangesAsync();
		}

		public async Task UpdateNhanVien(NhanVien nv)
		{
			_context.NhanViens.Update(nv);
			await _context.SaveChangesAsync();
		}
	}
}
