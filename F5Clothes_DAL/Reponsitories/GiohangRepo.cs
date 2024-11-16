using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Reponsitories
{
    public class GiohangRepo : IGioHangRepo
    {
        private readonly DbduAnTnContext _context;

        public GiohangRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        private async Task<bool> CartItemExists(Guid Id, Guid IdSp)
        {
            return await _context.GioHangChiTiets
                .AsNoTracking()
                .AnyAsync(x => x.IdSpct == IdSp && x.IdGh == Id);
        }

        public async Task<GioHangChiTiet?> AddItem(GioHangChiTietDtos itemToAdd)
        {
            // Kiểm tra xem mục đã tồn tại trong giỏ hàng chưa
            if (!await CartItemExists(itemToAdd.IdGh, itemToAdd.IdSpct))
            {
                // Lấy thông tin sản phẩm chi tiết từ CSDL
                var sanPhamChiTiet = await _context.SanPhamChiTiets
                    .FirstOrDefaultAsync(x => x.Id == itemToAdd.IdSpct);

                if (sanPhamChiTiet == null)
                {
                    // Không tìm thấy sản phẩm chi tiết
                    throw new KeyNotFoundException($"Không tìm thấy sản phẩm chi tiết với Id: {itemToAdd.IdSpct}");
                }

                // Tạo đối tượng giỏ hàng chi tiết
                var item = new GioHangChiTiet
                {
                    IdGh = itemToAdd.IdGh,
                    IdSpct = sanPhamChiTiet.Id,
                    SoLuong = itemToAdd.SoLuong,
                    NgayTao = DateTime.UtcNow
                };

                try
                {
                    // Thêm mục vào cơ sở dữ liệu
                    var result = await _context.AddAsync(item);
                    await _context.SaveChangesAsync();

                    // Trả về mục vừa thêm
                    return result.Entity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi thêm mục vào giỏ hàng: {ex.Message}");
                    throw new Exception("Đã xảy ra lỗi khi thêm mục vào giỏ hàng.", ex);
                }
            }

            // Mục đã tồn tại trong giỏ hàng
            return null;
        }





        public async Task<IEnumerable<GioHangChiTiet>> GetAll(Guid userId)
        {
            var result = await _context.GioHangChiTiets
                .AsNoTracking()
                .Include(c => c.IdGhNavigation)
                .Include(c => c.IdSpctNavigation)
                .Where(c => c.IdGhNavigation != null && c.IdGhNavigation.IdKh == userId)
                .ToListAsync();

            if (result.Any(c => c.IdGhNavigation == null || c.IdSpctNavigation == null))
            {
                Console.WriteLine($"Cảnh báo: Có thuộc tính điều hướng null cho UserId: {userId}");
            }

            return result;
        }


        public async Task<GioHangChiTiet?> GetItem(Guid id)
        {
            var item = await _context.GioHangChiTiets
                .Include(c => c.IdSpctNavigation)
                .ThenInclude(spct => spct.IdSpNavigation)
                .FirstOrDefaultAsync(x => x.IdGh == id);

            // Kiểm tra nếu item hoặc các liên kết quan trọng là null
            if (item == null || item.IdSpctNavigation?.IdSpNavigation == null)
            {
                return null; // Hoặc ném ra một exception tùy thuộc vào cách xử lý lỗi của bạn
            }

            return item;
        }



        public async Task<bool> RemoveItem(Guid id)
        {
            var cartItem = await _context.GioHangChiTiets
                .Include(x => x.IdSpctNavigation)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cartItem != null)
            {
                _context.GioHangChiTiets.Remove(cartItem);
                await _context.SaveChangesAsync();
                return true; // Indicating success
            }
            return false; // Indicating failure
        }


        public async Task<GioHangChiTiet> UpdateItem(Guid cartItemId, GioHangUpdate itemToUpdate)
        {
            var cartItem = await _context.GioHangChiTiets
                .Include(x => x.IdSpctNavigation)
                .SingleOrDefaultAsync(x => x.Id == cartItemId);

            if (cartItem is not null)
            {
                cartItem.SoLuong = itemToUpdate.SoLuong;
                await _context.SaveChangesAsync();
            }
            return cartItem;
        }
    }
}
