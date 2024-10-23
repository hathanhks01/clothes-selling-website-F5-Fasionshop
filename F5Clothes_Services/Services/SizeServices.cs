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
    public class SizeServices : ISizeServices
    {
        private readonly ISizeRepo _sizeRepo;
        public SizeServices(ISizeRepo sizeRepo) 
        { 
            _sizeRepo = sizeRepo;
        }

        public async Task<Size> AddSize(SizeDtos sizeDto)
        {
            return await _sizeRepo.AddSize(sizeDto);
        }

        public async Task DeleteSize(Guid id)
        {
            await _sizeRepo.DeleteSize(id);
        }

        public async Task<List<Size>> GetAllSize()
        {
            return await _sizeRepo.GetAllSize();
        }

        public async Task<Size> GetByIdSize(Guid id)
        {
            return await _sizeRepo.GetByIdSize(id);
        }

        public async Task<Size> UpdateSize(SizeDtos sizeDto)
        {
            return await _sizeRepo.UpdateSize(sizeDto);
        }
    }
}
