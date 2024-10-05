using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IGiohangChiTietRepo
    {
        Task<List<GioHangChiTiet>> GetAllGHCT();
        Task<GioHangChiTiet> GetByGHCT(Guid id);
        Task AddGhct(GioHangChiTiet ghct);
        Task UpdateGhct(GioHangChiTiet ghct);
        Task DeleteGhct(Guid Id);
    }
}
