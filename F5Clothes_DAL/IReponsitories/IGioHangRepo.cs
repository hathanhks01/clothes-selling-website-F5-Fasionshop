using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IGioHangRepo
    {
        Task<List<GioHang>> GetAllGioHang();
        Task<GioHang> GetByGioHang(Guid id);
        Task AddGh(GioHang gh);
        Task UpdateGh(GioHang gh);
        Task DeleteGh(Guid Id);
    }
}
