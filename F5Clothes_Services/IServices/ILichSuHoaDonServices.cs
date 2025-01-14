﻿using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface ILichSuHoaDonServices
    {
        Task<List<LichSuHoaDon>> GetAll();
        Task<LichSuHoaDon> GetById(Guid id);
        Task Create(LichSuHoaDon LsHoaDon);
        Task Update(LichSuHoaDon LsHoaDon);
        Task Delete(Guid id);
    }
}
