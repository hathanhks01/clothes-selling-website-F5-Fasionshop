using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IKhachhangRepo
    {
        Task<List<KhachHang>> GetAllKhachHang();
        Task<KhachHang> GetByKhachHang(Guid id);
        Task AddKh(KhachHang Kh);
        Task UpdateKh(KhachHang Kh);
        Task DeleteKh(Guid Id);
    }
}
