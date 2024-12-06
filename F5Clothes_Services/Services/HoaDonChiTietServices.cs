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
    public class HoaDonChiTietServices : IHoaDonChiTietServices
    {
        private readonly IHoaDonChiTietServices _hdctRepo;
        public HoaDonChiTietServices(IHoaDonChiTietServices hdctRepo)
        {
            _hdctRepo = hdctRepo;   
        }
        public async Task Create(HoaDonChiTiet hoaDonct)
        {
           await _hdctRepo.Create(hoaDonct);
        }

        public async Task Delete(Guid id)
        {
            await _hdctRepo.Delete(id);
        }

        public async Task<List<HoaDonChiTiet>> GetAll()
        {
            return await _hdctRepo.GetAll();
        }

        public Task<HoaDonChiTiet> GetById(Guid id)
        {
            return _hdctRepo.GetById(id);
        }

        public async Task Update(HoaDonChiTiet hoaDonct)
        {
            await _hdctRepo.Update(hoaDonct);
        }
    }
}
