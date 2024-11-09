using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface ISanPhamChiTietServices
    {
        Task<List<SanPhamChiTiet>> GetAllSanPhamChiTiet();
        Task<SanPhamChiTiet> GetByIdSanPhamChiTiet(Guid id);
        Task<SanPhamChiTiet> AddSanPhamChiTiet(SanPhamChiTietDtos sanPhamChiTietDto);
        Task<SanPhamChiTiet> UpdateSanPhamChiTiet(SanPhamChiTietDtos sanPhamChiTietDto);
        Task DeleteSanPhamChiTiet(Guid id);
    }
}
