using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.Services
{
    public class SanPhamServices : ISanPhamServices
    {
        private readonly ISanPhamRepo _sanPhamRepo;
        public SanPhamServices(ISanPhamRepo sanPhamRepo)
        {
            _sanPhamRepo = sanPhamRepo;
        }
        public async Task<SanPham> AddSanPham(SanPhamDtos sanPhamDto)
        {
            return await _sanPhamRepo.AddSanPham(sanPhamDto);
        }

        public async Task DeleteSanPham(Guid id)
        {
            await _sanPhamRepo.DeleteSanPham(id);
        }

        public async Task<IEnumerable<object>> GetAllSanPham()
        {
            return await _sanPhamRepo.GetAllSanPham();
        }

        public async Task<SanPham> GetByIdSanPham(Guid id)
        {
            return await _sanPhamRepo.GetByIdSanPham(id);
        }

        public async Task<SanPham> UpdateSanPham(SanPhamDtos sanPhamDto)
        {
            return await _sanPhamRepo.UpdateSanPham(sanPhamDto);
        }
    }
}
