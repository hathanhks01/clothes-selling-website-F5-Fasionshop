using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface INhanVienServices
    {
        Task<List<NhanVien>> GetAll();
        Task<NhanVien> GetById(Guid id);
        Task Create(NhanVien nhanVien);
        Task Update(NhanVien nhanVien);
        Task Delete(Guid id);
    }
}
