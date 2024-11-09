using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IDanhMucService
    {
        Task<List<DanhMuc>> GetAllDanhMuc();
        Task<DanhMuc> GetByIdDanhMuc(Guid id);
        Task<DanhMuc> AddDanhMuc(DanhMucDtos danhMucDto);
        Task<DanhMuc> UpdateDanhMuc(DanhMucDtos danhMucDto);
        Task DeleteDanhMuc(Guid id);
    }
}
