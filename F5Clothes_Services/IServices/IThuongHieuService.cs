using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IThuongHieuService
    {
        Task<List<ThuongHieu>> GetAllThuongHieu();
        Task<ThuongHieu> GetByIdThuongHieu(Guid id);
        Task<ThuongHieu> AddThuongHieu(ThuongHieuDtos thuongHieuDto);
        Task<ThuongHieu> UpdateThuongHieu(ThuongHieuDtos thuongHieuDto);
        Task DeleteThuongHieu(Guid id);

    }
}
