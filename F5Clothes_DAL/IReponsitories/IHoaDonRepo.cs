using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IHoaDonRepo
    {
        Task<List<HoaDon>> GetAllHoaDon();
        Task<object> GetByHoaDon(Guid id);
        Task<HoaDon> AddHd(HoaDon Hd);
        Task AddHdgioHang(HoaDon Hd);
        Task UpdateHd(HoaDon Hd);
        Task DeleteHd(Guid Id);
        Task<string> GenerateMaHoaDon();
    }
}
