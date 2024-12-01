using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;

using F5Clothes_Services.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IGioHangServices
    {
        Task<List<GiohangDtos>> GetAllGioHangAsync(Guid idKh);
        Task<GioHang> GetByGioHang(Guid idKh);
        Task<GiohangDtos> GetGioHangByIdAsync(Guid id);
        Task AddGioHangAsync(AddGioHangDtos addDto);
        Task UpdateGioHangAsync(GioHangUpdate updateDto);
        Task DeleteGioHangAsync(Guid id);
        Task PlaceOrderAsync(Guid customerId, OrderInfoDto orderInfo);
        Task<decimal?> ApplyVoucherAsync(OrderInfoDto orderInfo, decimal tongTien);
    }
}
