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

        // Get all cart items for a specific customer
        public async Task<List<GiohangDtos>> GetAllGioHangAsync(Guid idKh)
        {
            var existingCart = await _context.GioHangs
         .FirstOrDefaultAsync(gh => gh.IdKh == idKh);

            // If no cart exists, create one
            if (existingCart == null)
            {
                var newCart = new GioHang
                {
                    Id = Guid.NewGuid(), // Generate a new unique ID for the cart
                    IdKh = idKh,
                    NgayTao = DateTime.UtcNow, // Add the current timestamp
                                               // Add other default values as needed
                };
                _context.GioHangs.Add(newCart);
                await _context.SaveChangesAsync();

                // Set the new cart for the customer
                existingCart = newCart;
            }

            // Fetch cart details
            var gioHangDtos = await _context.GioHangChiTiets
                .Where(ghct => ghct.IdGh == existingCart.Id)
                .Include(ghct => ghct.IdSpctNavigation)
                    .ThenInclude(spct => spct.IdSpNavigation)
                .Select(ghct => new GiohangDtos
                {
                    IdGh = ghct.IdGh,
                    IdSpct = ghct.IdSpct,
                    TenSp = ghct.IdSpctNavigation.IdSpNavigation.TenSp,
                    HinhAnh = ghct.IdSpctNavigation.IdSpNavigation.ImageDefaul,
                    TenMauSac = ghct.IdSpctNavigation.IdMsNavigation.TenMauSac,
                    TenSize = ghct.IdSpctNavigation.IdSizeNavigation.TenSize,
                    SoLuong = ghct.SoLuong,
                    DonGia = ghct.DonGia,
                    TongTien = ghct.SoLuong * ghct.DonGia
                })
                .ToListAsync();

            return gioHangDtos;
        }

        // Get a specific cart item by its ID
        public async Task<GiohangDtos> GetGioHangByIdAsync(Guid id)
        {
            var gioHangDto = await _context.GioHangChiTiets
                .Where(ghct => ghct.Id == id)
                .Include(ghct => ghct.IdSpctNavigation)
                    .ThenInclude(spct => spct.IdSpNavigation)
                .Select(ghct => new GiohangDtos
                {
                    IdGh = ghct.IdGh,
                    IdSpct = ghct.IdSpct,
                    TenSp = ghct.IdSpctNavigation.IdSpNavigation.TenSp,
                    HinhAnh = ghct.IdSpctNavigation.IdSpNavigation.ImageDefaul,
                    TenMauSac = ghct.IdSpctNavigation.IdMsNavigation.TenMauSac,
                    TenSize = ghct.IdSpctNavigation.IdSizeNavigation.TenSize,
                    SoLuong = ghct.SoLuong,
                    DonGia = ghct.DonGia,
                    TongTien = ghct.SoLuong * ghct.DonGia
                })
                .FirstOrDefaultAsync();

            return gioHangDto;
        }

        // Get the price of a product variant
        public async Task<decimal> GetProductPriceAsync(Guid idSpct)
        {
            return (decimal)await _context.SanPhamChiTiets
                    .Where(spct => spct.Id == idSpct)
                    .Select(spct => spct.IdSpNavigation.GiaBan)
                    .FirstOrDefaultAsync();
        }
        public async Task<GioHangChiTiet> GetCartItemByIdsAsync(Guid idGh, Guid idSpct)
        {
            return await _context.GioHangChiTiets
                .FirstOrDefaultAsync(g => g.IdGh == idGh && g.IdSpct == idSpct);
        }
        // Add a new item to the cart
        public async Task AddGioHangAsync(GioHangChiTiet newCartItem)
        {
            _context.GioHangChiTiets.Add(newCartItem);
            await _context.SaveChangesAsync();
        }

        // Update a cart item
        public async Task UpdateGioHangAsync(GioHangChiTiet updatedCartItem)
        {
            _context.GioHangChiTiets.Update(updatedCartItem);  // Cập nhật bản ghi trong cơ sở dữ liệu
            await _context.SaveChangesAsync();  // Lưu thay đổi vào database
        }


        // Delete a cart item
        public async Task DeleteGioHangAsync(Guid id)
        {
            var gioHangChiTiet = await _context.GioHangChiTiets.FindAsync(id);
            if (gioHangChiTiet != null)
            {
                _context.GioHangChiTiets.Remove(gioHangChiTiet);
                await _context.SaveChangesAsync();
            }
        }
    }
}
