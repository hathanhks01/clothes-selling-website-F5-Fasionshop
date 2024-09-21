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

        public async Task AddSanPham(SanPham sanPham)
        {
            // Fetch related entities by their IDs
            var danhMuc = _context.DanhMucs.FirstOrDefault(a => a.Id == sanPham.IdDm);
            var xuatXu = _context.XuatXus.FirstOrDefault(a => a.Id == sanPham.IdXx);
            var giamGia = _context.GiamGia.FirstOrDefault(a => a.Id == sanPham.IdGg);
            var thuongHieu = _context.ThuongHieus.FirstOrDefault(a => a.Id == sanPham.IdTh);
            var chatLieu = _context.ChatLieus.FirstOrDefault(a => a.Id == sanPham.IdCl);

            // Check if the required entities exist
            if (danhMuc == null || xuatXu == null || giamGia == null || thuongHieu == null || chatLieu == null)
            {
                // Handle error (e.g., return, throw exception, etc.)
                throw new Exception("One or more required entities were not found.");
            }

            // Optionally: You can associate these entities with the SanPham object if needed
            sanPham.IdDmNavigation = danhMuc;
            sanPham.IdXxNavigation = xuatXu;
            sanPham.IdGgNavigation = giamGia;
            sanPham.IdThNavigation = thuongHieu;
            sanPham.IdClNavigation= chatLieu;

            // Add the SanPham entity to the context
            _context.SanPhams.Add(sanPham);

            // Save changes asynchronously
            await _context.SaveChangesAsync();
        }


        public async Task<List<SanPham>> GetAllSanPham()
        {
            return await _context.SanPhams.OrderBy(p=>p.MaSp).ToListAsync();
        }

        public async Task DeleteSanPham(string Id)
        {
            var sp = await GetBySanPham(Id);
            _context.SanPhams.Remove(sp);
            await _context.SaveChangesAsync();  
        }

        public async Task<SanPham> GetBySanPham(string Id)
        {
            return await _context.SanPhams.FirstOrDefaultAsync(x=> x.MaSp==Id);
        }

        public async Task UpdateSanPham(SanPham sanPham)
        {
           _context.Entry(sanPham).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
