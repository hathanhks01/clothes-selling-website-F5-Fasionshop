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

        public async Task<string> GenerateSerialForDate(string prefix, string datePart)
        {
            string fullPrefix = $"{prefix}{datePart}";

            // Gọi repository để đếm số lượng hóa đơn với tiền tố này
            int count = await _hoaDonRepo.CountHdByMaHoaDonPrefix(fullPrefix);

            // Serial bắt đầu từ 1, cần định dạng thành 2 chữ số (01, 02, ...)
            return (count + 1).ToString("D2");
        }

        public async Task<List<HoaDon>> GetAll()
        {
            return await _hoaDonRepo.GetAllHoaDon(); 
        }

        public async Task<HoaDon> GetById(Guid id)
        {
            return await _hoaDonRepo.GetByHoaDon(id);
        }

        public async Task Update(HoaDon hoaDon)
        {
            await _hoaDonRepo.UpdateHd(hoaDon);
        }
      
    }
}
