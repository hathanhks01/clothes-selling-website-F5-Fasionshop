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
            var existingCart = await GetByGioHang(idKh); // Ensure the cart exists or is created.

            // Retrieve all details of the cart
            var gioHangDtos = await _context.GioHangChiTiets
                .Where(ghct => ghct.IdGh == existingCart.Id)
                .Include(ghct => ghct.IdSpctNavigation)
                    .ThenInclude(spct => spct.IdSpNavigation)
                .Include(ghct => ghct.IdSpctNavigation.IdMsNavigation)  // Include color information
                .Include(ghct => ghct.IdSpctNavigation.IdSizeNavigation) // Include size information
                .Select(ghct => new GiohangDtos
                {
                    Id = ghct.Id,
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

            Console.WriteLine("Chi tiết giỏ hàng đã được lấy thành công.");
            return gioHangDtos;
        }



        public async Task<GioHang> GetByGioHang(Guid idKh)
        {
            // Fetch the existing cart for the given customer ID
            var existingCart = await _context.GioHangs
                .FirstOrDefaultAsync(g => g.IdKh == idKh);

            // If no cart exists for the customer, create a new one
            if (existingCart == null)
            {
                var newCart = new GioHang
                {
                    Id = Guid.NewGuid(),  // Generate a new unique ID for the cart
                    IdKh = idKh,
                    NgayTao = DateTime.UtcNow,  // Set the creation date to now
                };

                _context.GioHangs.Add(newCart);  // Add the new cart to the context
                await _context.SaveChangesAsync();  // Save the changes to the database

                return newCart;  // Return the newly created cart
            }

            // Return the existing cart if it already exists
            return existingCart;
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
            // Find the existing cart item
            var existingCartItem = await _context.GioHangChiTiets
                .FirstOrDefaultAsync(ghct => ghct.Id == updatedCartItem.Id);

            if (existingCartItem == null)
            {
                throw new Exception("Cart item not found.");
            }

            // Update the fields with the new values
            existingCartItem.SoLuong = updatedCartItem.SoLuong;
            existingCartItem.NgayCapNhat = DateTime.Now;
            // You can set other properties explicitly if needed, like DonGia, IdSpct, etc.

            // Save the changes
            await _context.SaveChangesAsync();
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
            if (gioHangChiTiet == null) throw new Exception("Cart item not found.");
        }

        public async Task<GioHangChiTiet> GetGioHangById(Guid id)
        {
            return await _context.GioHangChiTiets.FirstOrDefaultAsync(ghct => ghct.Id == id);
        }
    }
}
