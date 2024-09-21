using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface ILSHDRepo
    {
        Task<List<LichSuHoaDon>> GetAllLichSuHoaDon();
        Task<LichSuHoaDon> GetByLichSuHoaDon(Guid id);
        Task AddLs(LichSuHoaDon Ls);
        Task UpdateLs(LichSuHoaDon Ls);
        Task DeleteLs(Guid Id);
    }
}
