using AutoMapper;

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
    public class KhachHangService : IKhachHangService
    {
        private readonly IKhachhangRepo _khachhangRepo;
        private readonly IMapper _mapper;

        public KhachHangService(IKhachhangRepo khachhangRepo, IMapper mapper)
        {
            _khachhangRepo = khachhangRepo;
            _mapper = mapper;
        }
        public async Task<bool> ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            return await _khachhangRepo.ChangePassword(id, oldPassword, newPassword);
        }

        public async Task DeleteChatLieu(Guid id)
        {
            await _khachhangRepo.DeleteKh(id);

        }

        public Task<List<KhachHang>> GetAllKhachHang()
        {
           return _khachhangRepo.GetAllKhachHang();
        }

        public Task<KhachHang?> GetByIdKhachHang(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<KhachHang> GetByKhachHang(Guid id)
        {
            return await _khachhangRepo.GetByKhachHang(id);
        }





        public async Task<KhachHang> UpdateKhachHang(KhachHangDtos Kh)
        {
            var khachHang = _mapper.Map<KhachHang>(Kh);
           await _khachhangRepo.UpdateKh(khachHang);
            return khachHang;
           
        }

    }
}
