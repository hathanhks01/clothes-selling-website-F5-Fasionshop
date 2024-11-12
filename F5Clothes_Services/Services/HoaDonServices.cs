﻿using F5Clothes_DAL.IReponsitories;
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
        public HoaDonServices(IHoaDonRepo repo)
        {
            _hoaDonRepo = repo;
        }
        public async Task Create(HoaDon hoaDon)
        {
                if (hoaDon == null)
                {
                    throw new ArgumentNullException(nameof(hoaDon), "Hóa đơn không được để null.");
                }

                // Gán ngày tạo là ngày hiện tại
                hoaDon.NgayTao = DateTime.Now; // Lưu ngày giờ thực tế

                // Kiểm tra và tính tổng tiền cho hóa đơn từ danh sách chi tiết hóa đơn
                if (hoaDon.HoaDonChiTiets != null && hoaDon.HoaDonChiTiets.Count > 0)
                {
                    decimal tongThanhTien = 0;

                    // Tính thành tiền cho từng chi tiết hóa đơn và tổng tiền cho hóa đơn
                    foreach (var chiTiet in hoaDon.HoaDonChiTiets)
                    {
                        decimal thanhTien = (chiTiet.SoLuong * chiTiet.DonGia) ?? 0; // Nếu DonGia là nullable // Thành tiền = Số lượng * Đơn giá

                        // Cộng dồn thành tiền vào tổng
                        tongThanhTien += thanhTien;
                    }

                    // Gán tổng tiền vào hóa đơn
                    hoaDon.ThanhTien = tongThanhTien; // Bạn cần đảm bảo rằng hoaDon có thuộc tính ThanhTien
                }
                else
                {
                    hoaDon.ThanhTien = 0; 
                }
                await _hoaDonRepo.AddHd(hoaDon);
            
        }

        public async Task Delete(Guid id)
        {
          await _hoaDonRepo.DeleteHd(id);
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