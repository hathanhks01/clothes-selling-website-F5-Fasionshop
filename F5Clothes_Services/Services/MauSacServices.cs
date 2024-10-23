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
    public class MauSacServices : IMauSacServices
    {
        private readonly IMauSacRepo _mauSacRepo;
        public MauSacServices(IMauSacRepo mauSacRepo)
        {
            _mauSacRepo = mauSacRepo;
        }
        public async Task<MauSac> AddMauSac(MauSacDtos mauSacDto)
        {
            return await _mauSacRepo.AddMauSac(mauSacDto);    
        }

        public async Task DeleteMauSac(Guid id)
        {
            await _mauSacRepo.DeleteMauSac(id);
        }

        public Task<List<MauSac>> GetAllMauSac()
        {
            return _mauSacRepo.GetAllMauSac();
        }

        public Task<MauSac> GetByIdMauSac(Guid id)
        {
            return _mauSacRepo.GetByIdMauSac(id);
        }

        public Task<MauSac> UpdateMauSac(MauSacDtos mauSacDto)
        {
            return _mauSacRepo.UpdateMauSac(mauSacDto);
        }
    }
}
