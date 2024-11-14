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
    public class GioHangServices : IGioHangServices
    {
        private readonly IGioHangRepo _gioHangRepo;

        public GioHangServices(IGioHangRepo gioHangRepo)
        {
            _gioHangRepo = gioHangRepo;
        }
        public Task<GioHangChiTiet> AddItem(GioHangChiTietDtos itemToAdd)
        {
            return _gioHangRepo.AddItem(itemToAdd);
        }

        public Task<IEnumerable<GioHangChiTiet>> GetAll(Guid userId)
        {
            return _gioHangRepo.GetAll(userId);
        }

        public Task<GioHangChiTiet> GetItem(Guid id)
        {
            return _gioHangRepo.GetItem(id);
        }

        public Task<GioHangChiTiet> RemoveItem(Guid id)
        {
            return _gioHangRepo.RemoveItem(id);
        }

        public Task<GioHangChiTiet> UpdateItem(Guid cartItemId, GioHangUpdate itemToUpdate)
        {
            return _gioHangRepo.UpdateItem(cartItemId, itemToUpdate);
        }
    }
}
