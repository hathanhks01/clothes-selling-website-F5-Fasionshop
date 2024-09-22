using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IHoaDonServices
    {
        Task<List<HoaDon>> GetAll();
        Task<HoaDon> GetById(Guid id);
        Task Create(HoaDon hoaDon);
        Task Update(HoaDon hoaDon);
        Task Delete(Guid id);
    }
}
