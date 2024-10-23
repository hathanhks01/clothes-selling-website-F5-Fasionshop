using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IKhachHangServices
    {
        Task<List<KhachHang>> GetAll();
        Task<KhachHang> GetById(Guid id);
        Task Create(KhachHang khachHang);
        Task Update(KhachHang khachHang);
        Task Delete(Guid id);
    }
}
