using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IKhachHangService
    {
        Task<List<KhachHang>> GetAllKhachHang();
        Task<KhachHang?> GetByIdKhachHang(Guid id);
        Task<KhachHang> UpdateKhachHang(KhachHangDtos chatLieuDto);
        Task DeleteChatLieu(Guid id);
    }
}
