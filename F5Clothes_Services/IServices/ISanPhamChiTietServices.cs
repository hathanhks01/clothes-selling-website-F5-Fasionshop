using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface
        ISanPhamChiTietServices
    {
        Task<List<SanPhamChiTiet>> GetAll();
        Task<SanPhamChiTiet> GetById(Guid id);
        Task Create(SanPhamChiTiet chatLieu);
        Task Update(SanPhamChiTiet chatLieu);
        Task Delete(Guid id);
    }
}
