using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IGioHangChiTietRepositories
    {
        Task<List<GioHangChiTiet>> GetAll();
        Task<GioHangChiTiet> GetById(Guid id);
        Task Create(GioHangChiTiet gioHangChiTiet);
        Task Update(GioHangChiTiet gioHangChiTiet);
        Task Delete(Guid id);
    }
}
