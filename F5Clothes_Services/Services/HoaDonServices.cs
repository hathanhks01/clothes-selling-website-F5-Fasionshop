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
    public class HoaDonServices : IHoaDonServices
    {
        private readonly IHoaDonRepo _hoaDonRepo;
        public HoaDonServices(IHoaDonRepo repo, IMapper mapper)
        {
            _hoaDonRepo = repo;
        }

        public async Task<HoaDon> Create(HoaDon hoaDon)
        {
            if (hoaDon == null)
            {
                throw new ArgumentNullException(nameof(hoaDon));
            }

            return await _hoaDonRepo.AddHd(hoaDon);
        }

        public async Task Delete(Guid id)
        {
          await _hoaDonRepo.DeleteHd(id);
        }

    

        public async Task<List<HoaDon>> GetAll()
        {
            return await _hoaDonRepo.GetAllHoaDon(); 
        }

        public async Task<object> GetById(Guid id)
        {
            return await _hoaDonRepo.GetByHoaDon(id);
        }

        public async Task<bool> UpdateHoaDonAsync(HoaDon hoaDon)
        {
            return await _hoaDonRepo.UpdateHd(hoaDon);
        }

        public async Task updateStatusAsync(HoaDon Hd)
        {
            await _hoaDonRepo.updateStatus(Hd);
        }
    }
}
