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
        Task<KhachHang> GetByKhachHang(Guid id);
        Task<KhachHang> UpdateKh(KhachHang Kh);
        Task<bool> ChangePassword(Guid id, string oldPassword, string newPassword);

    }
}
