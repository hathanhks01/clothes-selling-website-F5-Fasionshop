using F5Clothes_DAL.Models;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface ISanPhamRepo
    {
        Task<List<SanPham>> GetAllSanPham();
        Task<SanPham> GetBySanPham(Guid Id);
        Task <SanPham>  AddSanPham (SanPham sanPham);
        Task UpdateSanPham(SanPham sanPham);
        

    }
}
