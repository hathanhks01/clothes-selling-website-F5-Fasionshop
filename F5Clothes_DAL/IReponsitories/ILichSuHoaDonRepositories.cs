using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface ILichSuHoaDonRepositories
    {
        Task<List<LichSuHoaDon>> GetAll();
        Task<LichSuHoaDon> GetById(Guid id);
        Task Create(LichSuHoaDon lichSuHoaDon);
        Task Update(LichSuHoaDon lichSuHoaDon);
        Task Delete(Guid id);
    }
}
