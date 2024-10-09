using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface ISPCTRepo
    {
        Task<List<SanPhamChiTiet>> GetAllSanPhamChiTiet();
        Task<SanPhamChiTiet> GetBySanPhamChiTiet(Guid id);
        Task AddSPCT(SanPhamChiTiet SPCT);
        Task UpdateSPCT(SanPhamChiTiet SPCT);
        Task DeleteSPCT(Guid Id);
    }
}
