using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface ISanPhamServices
    {
        Task<List<SanPham>> GetAll();
        Task<SanPham> GetById(Guid id);
        Task Create(SanPham sanPham);
        Task Update(SanPham sanPham);
        Task Delete(Guid id);
    }
}
