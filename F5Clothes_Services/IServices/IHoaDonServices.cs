using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IHoaDonServices
    {
        Task<List<HoaDon>> GetAll();
        Task<object> GetById(Guid id);
        Task<HoaDon> Create(HoaDon hoaDon);
        Task<bool> UpdateHoaDonAsync(HoaDon hoaDon);
        Task updateStatusAsync(HoaDon Hd);
        Task Delete(Guid id);
    }
}
