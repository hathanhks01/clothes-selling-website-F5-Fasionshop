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
    public class XuatXuService : IXuatXuService
    {
        private readonly IXuatXuRepo _xuatXuRepo;
        public XuatXuService(IXuatXuRepo xuatXuRepo)
        {
            _xuatXuRepo = xuatXuRepo;
        }

        public async Task<XuatXu> AddXuatXu(XuatXuDtos xuatXuDto)
        {
           return await _xuatXuRepo.AddXuatXu(xuatXuDto);    
        }

        public async Task DeleteXuatXu(Guid id)
        {
            await _xuatXuRepo.DeleteXuatXu(id);
        }

        public async Task<List<XuatXu>> GetAllXuatXu()
        {
            return await _xuatXuRepo.GetAllXuatXu();
        }

        public async Task<XuatXu> GetByIdXuatXu(Guid id)
        {
            return await _xuatXuRepo.GetByIdXuatXu(id);
        }

        public async Task<XuatXu> UpdateXuatXu(XuatXuDtos xuatXuDto)
        {
            return await _xuatXuRepo.UpdateXuatXu(xuatXuDto);
        }
    }
}
