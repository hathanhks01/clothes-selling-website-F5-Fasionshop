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
    public class ThuongHieuService : IThuongHieuService
    {
        private readonly IThuongHieuRepo _thuongHieuRepo;
        public ThuongHieuService(IThuongHieuRepo thuongHieuRepo)
        {
            _thuongHieuRepo = thuongHieuRepo;   
        }

        public async Task<ThuongHieu> AddThuongHieu(ThuongHieuDtos thuongHieuDto)
        {
            return await _thuongHieuRepo.AddThuongHieu(thuongHieuDto);
        }

        public async Task DeleteThuongHieu(Guid id)
        {
            await _thuongHieuRepo.DeleteThuongHieu(id);
        }

        public async Task<List<ThuongHieu>> GetAllThuongHieu()
        {
           return await _thuongHieuRepo.GetAllThuongHieu();
        }

        public async Task<ThuongHieu> GetByIdThuongHieu(Guid id)
        {
            return await _thuongHieuRepo.GetByIdThuongHieu(id);
        }

        public async Task<ThuongHieu> UpdateThuongHieu(ThuongHieuDtos thuongHieuDto)
        {
            return await _thuongHieuRepo.UpdateThuongHieu(thuongHieuDto);
        }
    }

}
