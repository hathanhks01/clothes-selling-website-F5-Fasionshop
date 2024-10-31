using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Reponsitories
{
    public class SanPhamRepo : ISanPhamRepo
    {
        private readonly DbduAnTnContext _context;

        public SanPhamRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task<SanPham> AddSanPham(SanPham sanPham)
        {
            
           
            _context.SanPhams.Add(sanPham);

            // Save changes asynchronously
            await _context.SaveChangesAsync();
            return sanPham;

        }




        public async Task<List<SanPham>> GetAllSanPham()
        {
            return await _context.SanPhams
                .Include(sp => sp.IdDmNavigation) // Include DanhMuc
                .Include(sp => sp.IdGgNavigation) // Include GiamGium (if needed)
                .Include(sp => sp.IdThNavigation) // Include ThuongHieu
                .Include(sp => sp.IdXxNavigation) // Include XuatXu
                .Include(sp => sp.IdClNavigation)
                .ToListAsync();
        }




        public async Task<SanPham> GetBySanPham(Guid Id)
        {
            return await _context.SanPhams.FirstOrDefaultAsync(x=> x.Id==Id);
        }

        public async Task UpdateSanPham(SanPham sanPham)
        {
           _context.Entry(sanPham).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
