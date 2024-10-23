using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IHinhThucThanhToanRepositories
    {
        Task<List<HinhThucThanhToan>> GetAll();
        Task<HinhThucThanhToan> GetById(Guid id);
        Task Create(HinhThucThanhToan hinhThucThanhToan);
        Task Update(HinhThucThanhToan hinhThucThanhToan);
        Task Delete(Guid id);
    }
}
