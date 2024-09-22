using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IGioHangServices
    {
        Task<List<GioHang>> GetAll();
        Task<GioHang> GetById(Guid id);
        Task Create(GioHang gioHang);
        Task Update(GioHang gioHang);
        Task Delete(Guid id);
    }
}
