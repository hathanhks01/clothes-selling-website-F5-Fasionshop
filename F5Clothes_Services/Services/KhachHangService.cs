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
    public class KhachHangService : IKhachHangService
    {
        private readonly IKhachhangRepo _khachhangRepo;

        public KhachHangService(IKhachhangRepo khachhangRepo)
        {
            _khachhangRepo = khachhangRepo;
        }

        public async Task<bool> ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            return await _khachhangRepo.ChangePassword(id, oldPassword, newPassword);
        }

        public async Task<List<KhachHang>> GetAllKhachHang()
        {
            return await _khachhangRepo.GetAllKhachHang();
        }

        public async Task<KhachHang> GetByKhachHang(Guid id)
        {
            return await _khachhangRepo.GetByKhachHang(id);
        }

        

        public async Task<KhachHang> UpdateKh(KhachHang Kh)
        {
            await _khachhangRepo.UpdateKh(Kh);
            return Kh;
           
        }
    }
}
