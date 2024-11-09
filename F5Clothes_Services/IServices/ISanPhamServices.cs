using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface ISanPhamServices
    {
        Task<List<SanPham>> GetAllSanPham();
        Task<SanPham> GetByIdSanPham(Guid id);
        Task<SanPham> AddSanPham(SanPhamDtos sanPhamDto);
        Task<SanPham> UpdateSanPham(SanPhamDtos sanPhamDto);
        Task DeleteSanPham(Guid id);
    }
}
