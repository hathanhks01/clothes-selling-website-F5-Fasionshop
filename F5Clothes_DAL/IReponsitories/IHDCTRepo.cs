using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IHDCTRepo
    {
        Task<List<HoaDonChiTiet>> GetAllHoaDonChiTiet();
        Task<HoaDonChiTiet> GetByHoaDonChiTiet(Guid id);
        Task AddHDCT(HoaDonChiTiet HDCT);
        Task UpdateHDCT(HoaDonChiTiet HDCT);
        Task DeleteHDCT(Guid Id);
    }
}
