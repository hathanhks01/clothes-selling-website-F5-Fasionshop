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
    public class GiamGiaService : IGiamGiaService
    {
        private readonly IGiamGiaRepo _giamGiaRepo;
        public GiamGiaService(IGiamGiaRepo giamGiaRepo)
        {
            _giamGiaRepo = giamGiaRepo;
        }    
        public async Task<GiamGium> AddGiamGia(GiamGiaDtos giamGiaDto)
        {
            return await _giamGiaRepo.AddGiamGia(giamGiaDto);
        }

        public async Task DeleteGiamGia(Guid id)
        {
            await _giamGiaRepo.DeleteGiamGia(id);
        }

        public async Task<List<GiamGium>> GetAllGiamGia()
        {
            return await _giamGiaRepo.GetAllGiamGia();
        }

        public async Task<GiamGium> GetByIdGiamGia(Guid id)
        {
            return await _giamGiaRepo.GetByIdGiamGia(id);
        }

        public async Task<GiamGium> UpdateGiamGia(GiamGiaDtos giamGiaDto)
        {
            return await _giamGiaRepo.UpdateGiamGia(giamGiaDto);
        }
    }
}
