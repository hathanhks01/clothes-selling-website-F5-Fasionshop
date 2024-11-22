using F5Clothes_DAL.Models;
using F5Clothes_DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IGioHangRepo
    {
        Task<List<GiohangDtos>> GetAllGioHangAsync(Guid idKh);
        Task<GiohangDtos> GetGioHangByIdAsync(Guid id);
        Task<decimal> GetProductPriceAsync(Guid idSpct);
        Task<GioHangChiTiet> GetCartItemByIdsAsync(Guid idGh, Guid idSpct);
        Task AddGioHangAsync(GioHangChiTiet newCartItem);
        Task UpdateGioHangAsync(GioHangChiTiet updatedCartItem);
        Task DeleteGioHangAsync(Guid id);
    }
}
