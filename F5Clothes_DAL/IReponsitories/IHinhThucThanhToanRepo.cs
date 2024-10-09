using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IHinhThucThanhToanRepo
    {
        Task<List<HinhThucThanhToan>> GetAllHinhThucThanhToan();
        Task<HinhThucThanhToan> GetByHinhThucThanhToan(Guid id);
        Task AddHTt(HinhThucThanhToan HTt);
        Task UpdateHTt(HinhThucThanhToan HTt);
        Task DeleteHTt(Guid Id);
    }
}
