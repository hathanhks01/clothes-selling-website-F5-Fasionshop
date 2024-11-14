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

        public async Task<GioHangChiTiet> AddItem(GioHangChiTietDtos itemToAdd)
        {
            if (await CartItemExists(itemToAdd.IdGh, itemToAdd.IdSpct) == false)
            {
                var item = await
                    (from SanPhamChiTiet  in _context.SanPhamChiTiets
                     where SanPhamChiTiet.Id == itemToAdd.IdSpct
                     select new GioHangChiTiet
                     {
                         Id= new Guid(),
                         IdGh = itemToAdd.IdGh,
                         IdSpct = SanPhamChiTiet.Id,
                         SoLuong = itemToAdd.SoLuong,
                         NgayTao = DateTime.Now,
                         
                     })
                     .FirstOrDefaultAsync();


                if (item != null)
                {
                    var result = await _context.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }

        public async Task<IEnumerable<GioHangChiTiet>> GetAll(Guid userId)
        {
            return await _context.GioHangChiTiets
                .AsNoTracking()
                .Include(c => c.IdGhNavigation)
                .Include(c => c.IdSpctNavigation)
                .Where(c => c.IdGhNavigation.IdKh == userId)
                .ToListAsync();

        }

        public async Task<GioHangChiTiet> GetItem(Guid id)
        {
            return await _context.GioHangChiTiets
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.IdGhNavigation)
                .Include(x => x.IdSpctNavigation)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<GioHangChiTiet> RemoveItem(Guid id)
        {
            var cartItem = await _context.GioHangChiTiets
                .Include(x => x.IdSpctNavigation)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cartItem != null)
            {
                _context.GioHangChiTiets.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            return cartItem;

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
