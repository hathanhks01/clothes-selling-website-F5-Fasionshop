using F5Clothes_DAL.Models;
using F5Clothes_DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IGioHangRepo
    {
        // Adds a new item to the cart
        Task<GioHangChiTiet> AddItem(GioHangChiTietDtos itemToAdd);

        // Retrieves all cart items for a given user
        Task<IEnumerable<GioHangChiTiet>> GetAll(Guid userId);

        // Retrieves a specific cart item by its ID
        Task<GioHangChiTiet> GetItem(Guid id);

        // Removes an item from the cart by its ID
        Task<bool> RemoveItem(Guid id);

        // Updates the quantity of a cart item
        Task<GioHangChiTiet> UpdateItem(Guid cartItemId, GioHangUpdate itemToUpdate);
    }
}
