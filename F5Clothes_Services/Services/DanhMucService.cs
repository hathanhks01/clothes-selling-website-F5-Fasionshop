using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.Reponsitories;
using F5Clothes_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.Services
{
    public class DanhMucService : IDanhMucService
    {
        private readonly IDanhMucRepo _danhMucRepo;
        public DanhMucService(IDanhMucRepo danhMucRepo)
        {
            _danhMucRepo = danhMucRepo;
        }
        public async Task<DanhMuc> AddDanhMuc(DanhMucDtos danhMucDto)
        {
            return await _danhMucRepo.AddDanhMuc(danhMucDto);    
        }

        public async Task DeleteDanhMuc(Guid id)
        {
            await _danhMucRepo.DeleteDanhMuc(id);
        }

        public async Task<List<DanhMuc>> GetAllDanhMuc()
        {
           return await _danhMucRepo.GetAllDanhMuc();
        }

        public async Task<DanhMuc> GetByIdDanhMuc(Guid id)
        {
            return await _danhMucRepo.GetByIdDanhMuc(id);
        }

        public async Task<DanhMuc> UpdateDanhMuc(DanhMucDtos danhMucDto)
        {
            return await _danhMucRepo.UpdateDanhMuc(danhMucDto);
        }
    }
}
