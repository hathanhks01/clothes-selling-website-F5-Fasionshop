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
    public class SanPhamChiTietServices : ISanPhamChiTietServices
    {
        private readonly ISPCTRepo _sPCTRepo;
        public SanPhamChiTietServices(ISPCTRepo sPCTRepo)
        {
            _sPCTRepo = sPCTRepo;
        }

        public async Task<SanPhamChiTiet> AddSanPhamChiTiet(SanPhamChiTietDtos sanPhamChiTietDto)
        {
            return await _sPCTRepo.AddSanPhamChiTiet(sanPhamChiTietDto);
        }

        public async Task DeleteSanPhamChiTiet(Guid id)
        {
            await _sPCTRepo.DeleteSanPhamChiTiet(id);
        }

        public async Task<List<SanPhamChiTiet>> GetAllSanPhamChiTiet()
        {
            return await _sPCTRepo.GetAllSanPhamChiTiet();
        }

        public async Task<SanPhamChiTiet> GetByIdSanPhamChiTiet(Guid id)
        {
            return await _sPCTRepo.GetByIdSanPhamChiTiet(id);
        }

        public async Task<SanPhamChiTiet> UpdateSanPhamChiTiet(SanPhamChiTietDtos sanPhamChiTietDto)
        {
            return await _sPCTRepo.UpdateSanPhamChiTiet(sanPhamChiTietDto);
        }
    }
}
