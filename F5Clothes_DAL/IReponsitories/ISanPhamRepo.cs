using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface ISanPhamRepo
    {
        Task<IEnumerable<object>> GetAllSanPham();
        Task<SanPham> GetByIdSanPham(Guid id);
        Task<SanPhamChiTiet> GetByIdSanPhamChiTiet(Guid id);
        Task<SanPham> AddSanPham(SanPhamDtos sanPhamDto);
        Task<SanPham> UpdateSanPham(SanPhamDtos sanPhamDto);
        Task DeleteSanPham(Guid id);
        Task<IEnumerable<object>> GetAllSanPhamsWithDetailsAsync(); 
        Task<object> GetSanPhamWithDetailsAsync(Guid sanPhamId);
        Task<SanPhamChiTiet> AddOrUpdateSanPhamChiTiet(SanPhamChiTietDtos chiTietDtos);
        Task UpdateSanPhamChiTiet(Guid sanPhamId, IEnumerable<SanPhamChiTietDtos> chiTietDtos);
        Task<IEnumerable<SanPhamChiTiet>> GetSanPhamChiTietBySanPhamId(Guid sanPhamId);
        Task<IEnumerable<object>> GetAllImageBySanPham();
    }
}
