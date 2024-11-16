using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace F5Clothes_DAL.Reponsitories
{
    public class GiohangRepo : IGioHangRepo
    {
        private readonly DbduAnTnContext _context;

        public GiohangRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task AddAsync(GioHang gioHang)
        {
            await _context.GioHangs.AddAsync(gioHang);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var gioHang = await GetByIdAsync(id);
            if (gioHang == null) throw new ArgumentException("Không tìm thấy Giỏ hàng.");
            _context.GioHangs.Remove(gioHang);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GioHang>> GetAllAsync()
        {
            return await _context.GioHangs.ToListAsync();
        }

        public async Task<GioHang?> GetByIdAsync(Guid id)
        {
            return await _context.GioHangs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(GioHang gioHang)
        {
            _context.GioHangs.Update(gioHang);
            await _context.SaveChangesAsync();
        }

        public async Task<GioHang?> GetWithDetailsAsync(Guid id)
        {
            return await _context.GioHangs
                .Include(g => g.GioHangChiTiets)
                    .ThenInclude(ct => ct.IdSpctNavigation)
                        .ThenInclude(spct => spct.IdSpNavigation) // Bao gồm thông tin từ Sản phẩm
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<GioHang>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _context.GioHangs
                .Where(gh => gh.IdKh == customerId)  // Lọc theo IdKh (ID khách hàng)
                .Include(gh => gh.GioHangChiTiets)  // Bao gồm GioHangChiTiet của giỏ hàng
                    .ThenInclude(ct => ct.IdSpctNavigation)  // Bao gồm SanPhamChiTiet
                        .ThenInclude(spct => spct.IdSpNavigation)  // Bao gồm thông tin sản phẩm từ SanPhamChiTiet
                .ToListAsync();
        }
    }
}
