﻿using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IHoaDonChiTietServices
    {
        Task<List<HoaDonChiTiet>> GetAll();
        Task<HoaDonChiTiet> GetById(Guid id);
        Task Create(HoaDonChiTiet hoaDonct);
        Task Update(HoaDonChiTiet hoaDonct);
        Task Delete(Guid id);
    }
}
