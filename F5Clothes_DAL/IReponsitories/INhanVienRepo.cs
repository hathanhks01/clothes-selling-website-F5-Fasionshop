﻿using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface INhanVienRepo
    {
        Task<List<NhanVien>> GetAllNhanVien();
        Task<List<NhanVienDtos>> GetNhanVien(ListNhanVienModel nhanvien);
        Task<NhanVien> GetByNhanVien(Guid Id);
        Task AddNhanVien(NhanVien nv);
        Task UpdateNhanVien(NhanVien nv);
        Task DeleteNhanVien(Guid Id);
    }
}
