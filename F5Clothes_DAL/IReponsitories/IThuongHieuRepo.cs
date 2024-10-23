using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IThuongHieuRepo
    {
        Task<List<ThuongHieu>> GetAllThuongHieu();
        Task<ThuongHieu> GetByThuongHieu(Guid id);
        Task AddTh(ThuongHieu th);
        Task UpdateTh(ThuongHieu th);
        Task DeleteTh(Guid Id);
    }
}
